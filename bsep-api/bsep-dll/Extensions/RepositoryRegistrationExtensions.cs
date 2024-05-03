using bsep_dll.Contracts;
using bsep_dll.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace bsep_dll.Extensions;

public static class RepositoryRegistrationExtensions
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserIdentityRepository, UserIdentityRepository>();
    }
}