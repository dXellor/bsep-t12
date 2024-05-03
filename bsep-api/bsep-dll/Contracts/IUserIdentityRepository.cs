using bsep_dll.Models;

namespace bsep_dll.Contracts;

public interface IUserIdentityRepository: ICrudRepository<UserIdentity>
{
    Task<UserIdentity?> GetByEmailAsync(string email);
}