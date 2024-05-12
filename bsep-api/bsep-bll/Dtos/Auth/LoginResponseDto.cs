using bsep_bll.Dtos.Users;

namespace bsep_bll.Dtos.Auth;

public class LoginResponseDto
{
    public UserDto User { get; set; }
    public string AccessToken { get; set; }
    public RefreshToken? RefreshToken { get; set; }

    public LoginResponseDto(UserDto user, string accessToken, RefreshToken refreshToken)
    {
        User = user;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}