using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace bsep_api.Extensions.Auth;

public static class AuthExtensions
{
    public static string? GetClaim(this ClaimsPrincipal user, string claimName)
    {
        switch (claimName)
        {
            case "email":
                return user.Claims.FirstOrDefault(c => c.Type is JwtRegisteredClaimNames.Email or ClaimTypes.Email)?.Value;
            
            default:
                return null;
        }
    }
}