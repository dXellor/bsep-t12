using bsep_bll.Dtos.Users;

namespace bsep_bll.Contracts;

public interface IUserService: ICrudService<UserDto>
{
    Task<UserDto?> GetByEmailAsync(string email);
    Task<bool> ActivateUser(string email);
    Task<bool> BlockUser(string email);
    Task<bool> GenerateOtp(string email);
}