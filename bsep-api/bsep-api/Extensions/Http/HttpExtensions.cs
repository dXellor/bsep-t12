using bsep_bll.Dtos.Auth;

namespace bsep_api.Extensions.Http;

public static class HttpExtensions
{
    public static string? GetJwt(this HttpRequest request)
    {
        var authHeader = request.Headers.Authorization.ToString();
        if (string.IsNullOrEmpty(authHeader) || !authHeader.Contains("Bearer"))
            return null;
        return authHeader.Replace("Bearer ", "");
    }

    public static void SetRefreshTokenCookie(this HttpResponse response, RefreshToken refreshToken)
    {
        var cookieOptions = new CookieOptions()
        {
            HttpOnly = true,
            Expires = refreshToken.Expires,
            // TODO: Enable secure cookie when HTTPS is operational
            Path = "Auth/refresh"
        };
        response.Cookies.Append("refresh-token", refreshToken.Token, cookieOptions);
    }

    public static string? GetCookie(this HttpRequest request, string key)
    {
        request.Cookies.TryGetValue(key, out var cookie);
        return cookie;
    }
}