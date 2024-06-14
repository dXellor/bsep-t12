using AutoMapper;
using bsep_bll.Dtos;
using bsep_bll.Dtos.Advertisements;
using bsep_bll.Dtos.Users;
using bsep_dll.Helpers.Pagination;
using bsep_dll.Models;
using bsep_dll.Models.Enums;

namespace bsep_bll.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.Package, opt => opt.MapFrom(src => src.Package.ToString()));

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.Parse<UserRoleEnum>(src.Role)))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<UserTypeEnum>(src.Type)))
                .ForMember(dest => dest.Package, opt => opt.MapFrom(src => Enum.Parse<PackageTypeEnum>(src.Package)));

            CreateMap<User, UserRegistrationDto>().ReverseMap();

            CreateMap<RoleChange, RoleChangeDto>().ReverseMap();
            CreateMap<Advertisement, AdvertisementDto>().ReverseMap();
        }
    }
}