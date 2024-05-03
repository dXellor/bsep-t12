using AutoMapper;
using bsep_bll.Dtos.Users;
using bsep_dll.Helpers.Pagination;
using bsep_dll.Models;

namespace bsep_bll.MapperProfiles;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, UserRegistrationDto>().ReverseMap();
    }
}