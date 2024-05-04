using bsep_bll.Contracts;
using bsep_bll.Services;
using Microsoft.Extensions.DependencyInjection;

namespace bsep_bll.Extensions;

public static class ServiceRegistrationExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
    }
}