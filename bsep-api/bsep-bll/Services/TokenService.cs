using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using bsep_bll.Contracts;
using bsep_bll.Dtos.Auth;
using bsep_bll.Dtos.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace bsep_bll.Services;

public class TokenService: ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateAccessToken(UserDto user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Cryptography:Tokens:JwtSecretKey"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512); 
        var claims = new List<Claim>()
        {
            new (JwtRegisteredClaimNames.Email, user.Email),
            new (JwtRegisteredClaimNames.Aud, _configuration["Cryptography:Tokens:JwtAudience"]!),
            new (JwtRegisteredClaimNames.Iss, _configuration["Cryptography:Tokens:JwtIssuer"]!),
            new (ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(15), signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public RefreshToken GenerateRefreshToken()
    {
        var length = int.Parse(_configuration["Cryptography:Tokens:RefreshTokenLength"]!);
        var duration = int.Parse(_configuration["Cryptography:Tokens:RefreshTokenDuration"]!);
        var refreshToken = new RefreshToken()
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(length)),
            Expires = DateTime.UtcNow.AddDays(duration)
        };
        return refreshToken;
    }

    public JwtSecurityToken? ParseAndValidateAccessToken(string accessToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters()
        {
            ValidIssuer = _configuration["Cryptography:Tokens:JwtIssuer"],
            ValidAudience = _configuration["Cryptography:Tokens:JwtAudience"],
            ValidateLifetime = false,
            ValidateIssuer = true,
            ValidateAudience = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Cryptography:Tokens:JwtSecretKey"]!))
        };

        try
        {
            tokenHandler.ValidateToken(accessToken, validationParameters, out var validatedToken);
            return tokenHandler.ReadJwtToken(accessToken);
        }
        catch (Exception e)
        {
            return null;
        }
    }
}