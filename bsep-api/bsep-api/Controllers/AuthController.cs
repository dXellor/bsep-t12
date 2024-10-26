using bsep_api.Extensions.Auth;
using bsep_api.Extensions.Http;
using bsep_api.Helpers.Validation.Auth;
using bsep_api.Helpers.Validation.User;
using bsep_api.Middleware;
using bsep_bll.Contracts;
using bsep_bll.Dtos.Auth;
using bsep_bll.Dtos.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bsep_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authServiceService;
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;
        private readonly SignalRHub signalRHub;

        public AuthController(IAuthService authService, IUserService userService, ILogger<AuthController> logger)
        {
            _authServiceService = authService;
            _userService = userService;
            _logger = logger;
            signalRHub = new SignalRHub();
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
        {
            var validator = new UserRegistrationDtoValidator();
            var validationResult = await validator.ValidateAsync(registrationDto);
            if (!validationResult.IsValid) 
                return BadRequest(validationResult.Errors.First());
            
            var result = await _authServiceService.Register(registrationDto);
            if (result != null)
            {
                return Created("Success", result);
            }
            return BadRequest("Unable to register user");
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var validator = new LoginDtoValidator();
            var validationResult = await validator.ValidateAsync(loginDto);
            if (!validationResult.IsValid) 
                return BadRequest(validationResult.Errors.First());
            
            var result = await _authServiceService.Login(loginDto);
            if (result == null)
            {
                _logger.LogInformation("{@RequestName} by {@UserInfo} from {@IpAddress}", "Failed login", loginDto.Email, Request.HttpContext.Connection.RemoteIpAddress.ToString());
                return Unauthorized("Invalid credentials");
            }

            if (result.RedirectToTotp)
            {
                return Ok(result);
            }

            Response.SetRefreshTokenCookie(result.RefreshToken!);
            result.RefreshToken = null;
            return Ok(result);
        }

        [HttpPost("refresh")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RefreshAccessToken()
        {
            var accessToken = Request.GetJwt();
            var refreshToken = Request.GetCookie("refresh-token");
            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            {
                _logger.LogWarning("Refresh endpoint contacted without needed tokens from {@IpAddress}", Request.HttpContext.Connection.RemoteIpAddress.ToString());
                await signalRHub.SendMessage("Refresh endpoint contacted without needed tokens");
                return Unauthorized("Access token and Refresh token are not set");
            }
            
            var result = await _authServiceService.RefreshAccessToken(accessToken, refreshToken);
            if (result != null)
            {
                return Ok(result);
            }
            return Unauthorized("Invalid credentials");
        }
        
        [Authorize]
        [HttpGet("accessCheck")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUserByAccessToken()
        {
            var email = User.GetClaim("email");
            var result = await _userService.GetByEmailAsync(email);

            if (result == null)
                return Unauthorized();

            return Ok(result);
        }
        
        [HttpGet("recaptchaAssessment/{recaptchaToken}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecaptchaAssessment([FromRoute] string recaptchaToken)
        {
            var result = await _authServiceService.CreateReCaptchaAssessment(recaptchaToken);
            return Ok(result);
        }
        
        [HttpPost("totp")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ValidateTotp([FromBody] TotpDto totp)
        {
            var result = await _authServiceService.ValidateTotp(totp);
            if (result == null)
            {
                _logger.LogInformation("{@RequestName} by {@UserInfo} from {@IpAddress}", "Failed totp login", totp.Email, Request.HttpContext.Connection.RemoteIpAddress.ToString());
                return Unauthorized("Invalid totp");
            }
            
            Response.SetRefreshTokenCookie(result.RefreshToken!);
            result.RefreshToken = null;
            return Ok(result);
        }
        
        [Authorize]
        [HttpPost("enable2Fa")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Enable2Fa([FromBody] TotpDto totp)
        {
            var email = User.GetClaim("email");
            totp.Email = email;
            var result = await _authServiceService.ValidateTotpAndEnableTwoFactorAuth(totp);
            if (!result)
                return BadRequest("Invalid totp");
            
            return Ok(result);
        }
        
        [Authorize]
        [HttpGet("totpQr")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTotpSecretQr()
        {
            var email = User.GetClaim("email");

            var result = await _authServiceService.GetTotpSecretQr(email);
            if (result == null)
                return NotFound("2FA already enabled");
            
            return Ok(result);
        }

        [HttpPost("resetPassword")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var result = await _authServiceService.ResetPassword(resetPasswordDto);
            if (!result)
                return Unauthorized("Your password reset link seems to be invalid.");
            return Ok("Password reset successful");
        }

        [HttpPost("startPasswordReset/{email}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> StartPasswordReset(string email)
        {
            var result = await _authServiceService.StartPasswordReset(email);
            if (!result)
                return NotFound("User with the email " + email + " not found");
            return Ok("Password reset link sent to the email");
        }
    }
}
