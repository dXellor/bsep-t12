using bsep_bll.Dtos.Users;

namespace bsep_bll.Contracts;

public interface IAuthService
{
    Task<UserDto?> RegisterUser(UserRegistrationDto registrationDto);
}