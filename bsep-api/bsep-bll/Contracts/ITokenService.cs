using System.IdentityModel.Tokens.Jwt;
using bsep_bll.Dtos.Auth;
using bsep_bll.Dtos.Email;
using bsep_bll.Dtos.Users;
using Microsoft.Extensions.Configuration;

namespace bsep_bll.Contracts;

public interface ITokenService
{
    string GenerateAccessToken(UserDto user);
    RefreshToken GenerateRefreshToken();
    JwtSecurityToken? ParseAndValidateAccessToken(string accessToken);
    PasswordResetToken GeneratePasswordResetToken();
}