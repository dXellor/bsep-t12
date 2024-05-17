using bsep_bll.Dtos.Users;

namespace bsep_bll.Contracts;

public interface IUserService: ICrudService<UserDto>
{
    Task<UserDto?> GetByEmailAsync(string email);
    Task<UserDto> ChangeRoleAsync(RoleChangeDto request);
}