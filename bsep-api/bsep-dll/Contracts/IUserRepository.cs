using bsep_dll.Models;

namespace bsep_dll.Contracts;

public interface IUserRepository: ICrudRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User> ChangeRoleAsync(RoleChange request);
    Task<int> DeleteByEmailAsync(string email);
}