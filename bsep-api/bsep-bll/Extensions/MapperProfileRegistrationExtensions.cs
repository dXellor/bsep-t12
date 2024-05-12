using AutoMapper;
using bsep_bll.MapperProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace bsep_bll.Extensions;

public static class MapperProfileRegistrationExtensions
{
    public static void RegisterMapperProfiles(this IServiceCollection services)
    {
        var config = new MapperConfiguration(c =>
        {
            c.AddProfile<UserProfile>();
        });

        services.AddSingleton(config.CreateMapper());
    }
}