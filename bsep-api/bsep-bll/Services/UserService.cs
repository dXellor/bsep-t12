using AutoMapper;
using bsep_bll.Contracts;
using bsep_bll.Dtos.Users;
using bsep_dll.Contracts;
using bsep_dll.Helpers.Pagination;
using bsep_dll.Models;
using bsep_dll.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace bsep_bll.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserIdentityRepository _userIdentityRepository;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IUserIdentityRepository userIdentityRepository, IAdvertisementRepository advertisementRepository, IMapper mapper, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _userIdentityRepository = userIdentityRepository;
            _advertisementRepository = advertisementRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PagedList<UserDto>> GetAllAsync(QueryPageParameters queryParameters)
        {
            try
            {
                var results = await _userRepository.GetAllAsync(queryParameters);
                return PagedList<UserDto>.ConvertToDtoPagedList(
                        results.Select(u => _mapper.Map<User, UserDto>(u)).AsQueryable(),
                        results.CurrentPage,
                        results.PageSize);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public Task<UserDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> CreateAsync(UserDto newObject)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> UpdateAsync(UserDto updatedUser)
        {
            try
            {
                var existingUser = await _userRepository.GetByEmailAsync(updatedUser.Email);

                if (existingUser == null)
                {
                    _logger.LogWarning($"User with email {updatedUser.Email} not found.");
                    return null;
                }

                // Map updatedUser to existingUser
                _mapper.Map(updatedUser, existingUser);

                var updatedEntity = await _userRepository.UpdateAsync(existingUser);

                _logger.LogInformation($"User with email {updatedUser.Email} successfully updated.");

                return _mapper.Map<User, UserDto>(updatedEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating user with email {updatedUser.Email}: {ex.Message}");
                throw;
            }
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        
        public async Task<int> DeleteByEmailAsync(string email)
        {
            try
            {
                var result = await _userRepository.DeleteByEmailAsync(email);
                if (result == 0)
                {
                    _logger.LogWarning($"User with email {email} not found.");
                    return 0;
                }

                _logger.LogInformation($"User with email {email} successfully deleted.");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting user with email {email}: {ex.Message}");
                throw;
            }
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var result = await _userRepository.GetByEmailAsync(email);
            return _mapper.Map<User, UserDto>(result);
        }
        
        public async Task<UserDto> ChangeRoleAsync(RoleChangeDto request)
        {
            try
            {
                var updatedUser = await _userRepository.ChangeRoleAsync(_mapper.Map<RoleChangeDto, RoleChange>(request));

                if (updatedUser == null)
                {
                    _logger.LogWarning($"User with email {request.Email} not found.");
                    return null;
                }

                _logger.LogInformation($"User with email {request.Email} successfully updated role to {request.NewRole}.");

                return _mapper.Map<User, UserDto>(updatedUser);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating role for user with email {request.Email}: {ex.Message}");
                throw;
            }
        }
        
        public async Task<int> DeleteUserByEmailAsync(string email)
        {
            try
            {
                var user = await _userRepository.GetByEmailAsync(email);
            
                if (user == null)
                {
                    _logger.LogWarning($"User with email {email} not found.");
                    return 0;
                }
                if (!user.Package.ToString().Equals(PackageTypeEnum.Gold.ToString()))
                {
                    _logger.LogWarning($"User with email {email} does not have the gold package.");
                    return 0;
                }
                
                var advertisements = await _advertisementRepository.GetAdvertisementsByUserIdAsync(user.Id);
                foreach (var ad in advertisements)
                {
                    await _advertisementRepository.DeleteAsync(ad.Id);
                }
                
                var userIdentityResult = await _userIdentityRepository.DeleteByEmailAsync(email);
                var userResult = await _userRepository.DeleteByEmailAsync(email);

                if (userIdentityResult == 0 || userResult == 0)
                {
                    _logger.LogWarning($"User with email {email} not found.");
                    return 0;
                }

                _logger.LogInformation($"User with email {email} successfully deleted.");
                return userIdentityResult + userResult;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting user with email {email}: {ex.Message}");
                throw;
            }
        }

    }
}
