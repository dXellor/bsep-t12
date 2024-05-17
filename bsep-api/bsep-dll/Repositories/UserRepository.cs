using bsep_dll.Contracts;
using bsep_dll.Data;
using bsep_dll.Helpers.Pagination;
using bsep_dll.Helpers.QueryParameters;
using bsep_dll.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace bsep_dll.Repositories;

public class UserRepository: IUserRepository
{
    private readonly ILogger<UserRepository> _logger;
    private readonly DataContext _dataContext;
    private readonly DbSet<User> _users;

    public UserRepository(ILogger<UserRepository> logger, DataContext dataContext)
    {
        _logger = logger;
        _dataContext = dataContext;
        _users = dataContext.Users;
    }
    
    public async Task<PagedList<User>> GetAllAsync(QueryPageParameters queryParameters)
    {
        try
        {
            var userQueryParameters = (UserQueryParameters) queryParameters;
            var query = _users
                .AsNoTracking()
                .Where(u => u.Email.ToLower().Contains(userQueryParameters.Email.ToLower()))
                .OrderBy(u => u.Email);

            return await PagedList<User>.GetAsPagedList(query, userQueryParameters.PageNumber, userQueryParameters.PageSize);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            throw;
        }
    }

    public async  Task<User> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<User> CreateAsync(User newObject)
    {
        var user = await _users.AddAsync(newObject);
        await _dataContext.SaveChangesAsync();
        return user.Entity;
    }

    public async  Task<User> UpdateAsync(User updatedObject)
    {
        throw new NotImplementedException();
    }

    public async Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var query = _users
            .AsNoTracking()
            .Where(u => u.Email == email);

        return await query.FirstOrDefaultAsync();
    }
}