using System.IdentityModel.Tokens.Jwt;
using System.Transactions;
using AutoMapper;
using bsep_bll.Contracts;
using bsep_bll.Dtos.Auth;
using bsep_bll.Dtos.Users;
using bsep_dll.Contracts;
using bsep_dll.Data;
using bsep_dll.Models;
using bsep_dll.Models.Enums;
using Google.Api.Gax.ResourceNames;
using Google.Cloud.RecaptchaEnterprise.V1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace bsep_bll.Services;

public class AuthService: IAuthService
{
    private readonly IUserIdentityRepository _userIdentityRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly ILogger<AuthService> _logger;
    private readonly IConfiguration _configuration;

    public AuthService(IUserIdentityRepository userIdentityRepository, IUserRepository userRepository, IMapper mapper, ILogger<AuthService> logger, IConfiguration configuration, ITokenService tokenService)
    {
        _userIdentityRepository = userIdentityRepository;
        _userRepository = userRepository;
        _tokenService = tokenService;
        _mapper = mapper;
        _logger = logger;
        _configuration = configuration;
    }
    
    public async Task<UserDto?> Register(UserRegistrationDto registrationDto)
    {
        var userEntity = _mapper.Map<UserRegistrationDto, User>(registrationDto);
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            userEntity.Role = UserRoleEnum.Client;
            var newUser = await _userRepository.CreateAsync(userEntity);

            var userIdentity = UserIdentity.CreateUserIdentity(
                registrationDto.Email,
                registrationDto.Password,
                int.Parse(_configuration["Cryptography:Password:SaltLength"]!),
                int.Parse(_configuration["Cryptography:Password:Iterations"]!),
                int.Parse(_configuration["Cryptography:Password:OutputLength"]!));

            await _userIdentityRepository.CreateAsync(userIdentity);

            scope.Complete();
            return _mapper.Map<User, UserDto>(newUser);
        }
        catch (Exception e)
        {
            _logger.LogError("Unable to register user, rollback");
            return null;
        }
    }

    public async Task<LoginResponseDto?> Login(LoginDto loginDto)
    {
        var identity = await _userIdentityRepository.GetByEmailAsync(loginDto.Email, includeUser: true);
        if (identity == null || !identity.VerifyCredentials(loginDto.Email, loginDto.Password)) return null;

        return await GenerateTokenPair(identity);
    }

    public async Task<LoginResponseDto?> RefreshAccessToken(string accessToken, string refreshToken)
    {
        var jwt = _tokenService.ParseAndValidateAccessToken(accessToken);
        if (jwt == null)
            return null;

        var emailClaim = jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.Email);
        if (emailClaim == null || string.IsNullOrEmpty(emailClaim.Value))
            return null;

        var identity = await _userIdentityRepository.GetByEmailAsync(emailClaim.Value, includeUser: true);
        if (!identity!.VerifyRefreshToken(refreshToken))
            return null;

        return await GenerateTokenPair(identity);
    }

    public async Task<bool> CreateReCaptchaAssessment(string token)
    {
        var client = await RecaptchaEnterpriseServiceClient.CreateAsync();
        var projectName = new ProjectName("bsep-t12-2024");

        // Build the assessment request.
        var createAssessmentRequest = new CreateAssessmentRequest()
        {
            Assessment = new Assessment()
            {
                // Set the properties of the event to be tracked.
                Event = new Event()
                {
                    SiteKey = _configuration["ReCaptcha:SiteKey"],
                    Token = token,
                },
            },
            ParentAsProjectName = projectName
        };

        var response = await client.CreateAssessmentAsync(createAssessmentRequest);

        if (response.TokenProperties.Valid == false)
        {
            _logger.LogInformation("The CreateAssessment call failed because the token was: " + response.TokenProperties.InvalidReason.ToString());
            return false;
        }

        // see: https://cloud.google.com/recaptcha-enterprise/docs/interpret-assessment
        _logger.LogInformation("The reCAPTCHA score is: " + response.RiskAnalysis.Score);

        foreach (var reason in response.RiskAnalysis.Reasons)
        {
            Console.WriteLine(reason.ToString());
        }

        return response.RiskAnalysis.Score >= 0.6;
    }
    
    private async Task<LoginResponseDto?> GenerateTokenPair(UserIdentity identity)
    {
        var userDto = _mapper.Map<User, UserDto>(identity.User!);
        var accessToken = _tokenService.GenerateAccessToken(userDto);
        var refreshToken = _tokenService.GenerateRefreshToken();
        identity.SetRefreshToken(refreshToken.Token, refreshToken.Expires);
        await _userIdentityRepository.UpdateAsync(identity);

        return new LoginResponseDto(userDto, accessToken, refreshToken);
    }
}