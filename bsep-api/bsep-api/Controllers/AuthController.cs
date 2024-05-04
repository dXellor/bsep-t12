using bsep_api.Extensions.Http;
using bsep_api.Helpers.Validation.Auth;
using bsep_api.Helpers.Validation.User;
using bsep_bll.Contracts;
using bsep_bll.Dtos.Auth;
using bsep_bll.Dtos.Users;
using Microsoft.AspNetCore.Mvc;

namespace bsep_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authServiceService;

        public AuthController(IAuthService authService)
        {
            _authServiceService = authService;
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
                return Unauthorized("Invalid credentials");
            
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
                return Unauthorized("Access token and Refresh token are not set");
            
            var result = await _authServiceService.RefreshAccessToken(accessToken, refreshToken);
            if (result != null)
            {
                return Ok(result);
            }
            return Unauthorized("Invalid credentials");
        }
    }
}
