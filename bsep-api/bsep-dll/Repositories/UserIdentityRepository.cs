using bsep_dll.Contracts;
using bsep_dll.Data;
using bsep_dll.Helpers.Pagination;
using bsep_dll.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace bsep_dll.Repositories;

public class UserIdentityRepository: IUserIdentityRepository
{
    private readonly ILogger<UserIdentityRepository> _logger;
    private readonly DataContext _dataContext;
    private readonly DbSet<UserIdentity> _identities;
    
    public UserIdentityRepository(ILogger<UserIdentityRepository> logger, DataContext dataContext)
    {
        _logger = logger;
        _dataContext = dataContext;
        _identities = dataContext.Identities;
    }
    
    public Task<PagedList<UserIdentity>> GetAllAsync(QueryPageParameters queryParameters)
    {
        throw new NotImplementedException();
    }

    public Task<UserIdentity> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<UserIdentity> CreateAsync(UserIdentity newObject)
    {
        var identity = await _identities.AddAsync(newObject);
        await _dataContext.SaveChangesAsync();
        return identity.Entity;
    }

    public Task<UserIdentity> UpdateAsync(UserIdentity updatedObject)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<UserIdentity?> GetByEmailAsync(string email)
    {
        var query = _identities
            .AsNoTracking()
            .Where(i => i.Email.Equals(email));

        return await query.FirstOrDefaultAsync();
    }
}