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
        services.AddScoped<ITotpService, TotpService>();
        services.AddScoped<IAdvertisementService, AdvertisementService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IMonitoringService, MonitoringService>();
    }
}