using bsep_dll.Contracts;
using bsep_dll.Data;
using bsep_dll.Helpers.Pagination;
using bsep_dll.Helpers.QueryParameters;
using bsep_dll.Models;
using bsep_dll.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace bsep_dll.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly DataContext _dataContext;
        private readonly DbSet<User> _users;

        public UserRepository(ILogger<UserRepository> logger, DataContext dataContext, IConfiguration configuration)
        {
            _logger = logger;
            _dataContext = dataContext;
            _users = dataContext.Users;
            _configuration = configuration;
        }

        public async Task<PagedList<User>> GetAllAsync(QueryPageParameters queryParameters)
        {
            try
            {
                var userQueryParameters = (UserQueryParameters)queryParameters;
                var query = _users
                    .AsNoTracking()
                    .Where(u => u.Email.ToLower().Contains(userQueryParameters.Email.ToLower()))
                    .OrderBy(u => u.Email);

                var pagedList = await PagedList<User>.GetAsPagedList(query, userQueryParameters.PageNumber, userQueryParameters.PageSize);
                foreach (var user in pagedList)
                {
                    user.DecryptData(_configuration["Cryptography:Data:AesKey"]!,
                        _configuration["Cryptography:Data:AesIv"]!);
                }

                return pagedList;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> CreateAsync(User newObject)
        {
            newObject.EncryptData(_configuration["Cryptography:Data:AesKey"]!,_configuration["Cryptography:Data:AesIv"]!);
            await _users.AddAsync(newObject);
            await _dataContext.SaveChangesAsync();

            var decryptedUser = new User(newObject);
            decryptedUser.DecryptData(_configuration["Cryptography:Data:AesKey"]!,_configuration["Cryptography:Data:AesIv"]!);
            return decryptedUser;
        }

        public async Task<User> UpdateAsync(User updatedUser)
        {
            try
            {
                var existingUser = await _users.FirstOrDefaultAsync(u => u.Email == updatedUser.Email);

                if (existingUser == null)
                {
                    throw new KeyNotFoundException($"User with email {updatedUser.Email} not found.");
                }

                existingUser.DecryptData(_configuration["Cryptography:Data:AesKey"]!,_configuration["Cryptography:Data:AesIv"]!);
                existingUser.UpdateAllowedValues(updatedUser);
                existingUser.EncryptData(_configuration["Cryptography:Data:AesKey"]!,_configuration["Cryptography:Data:AesIv"]!);
                
                _dataContext.Users.Update(existingUser);

                await _dataContext.SaveChangesAsync();

                return updatedUser;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
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

            var result = await query.FirstOrDefaultAsync();
            result?.DecryptData(_configuration["Cryptography:Data:AesKey"]!,_configuration["Cryptography:Data:AesIv"]!);

            return result;
        }

        public async Task<User> ChangeRoleAsync(RoleChange request)
        {
            throw new NotImplementedException("Rework this method");
        }
    }
}
