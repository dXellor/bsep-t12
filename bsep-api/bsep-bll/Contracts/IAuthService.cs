using bsep_bll.Dtos.Auth;
using bsep_bll.Dtos.Users;

namespace bsep_bll.Contracts;

public interface IAuthService
{
    Task<UserDto?> Register(UserRegistrationDto registrationDto);
    Task<LoginResponseDto?> Login(LoginDto loginDto);
    Task<LoginResponseDto?> RefreshAccessToken(string accessToken, string refreshToken);
    Task<bool> CreateReCaptchaAssessment(string token);
    Task<LoginResponseDto> ValidateTotp(TotpDto totpDto);
    Task<bool> ValidateTotpAndEnableTwoFactorAuth(TotpDto totpDto);
    Task<byte[]> GetTotpSecretQr(string email);
    Task<bool> ResetPassword(ResetPasswordDto resetPasswordDto);
    Task<bool> StartPasswordReset(string email);
}