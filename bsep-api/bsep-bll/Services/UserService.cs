using AutoMapper;
using bsep_bll.Contracts;
using bsep_bll.Dtos.Users;
using bsep_dll.Contracts;
using bsep_dll.Helpers.Pagination;
using bsep_dll.Models;
using bsep_dll.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace bsep_bll.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserIdentityRepository _userIdentityRepository;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly IEmailService _emailService;

        public UserService(IUserRepository userRepository, IEmailService emailService, IUserIdentityRepository userIdentityRepository, IAdvertisementRepository advertisementRepository, IMapper mapper, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _userIdentityRepository = userIdentityRepository;
            _advertisementRepository = advertisementRepository;
            _mapper = mapper;
            _logger = logger;
            _emailService = emailService;
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
            var existingUser = await _userRepository.GetByEmailAsync(updatedUser.Email);

            if (existingUser == null)
            {
                return null;
            }

            // Map updatedUser to existingUser
            _mapper.Map(updatedUser, existingUser);

            var updatedEntity = await _userRepository.UpdateAsync(existingUser);
            return _mapper.Map<User, UserDto>(updatedEntity);
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
                    _logger.LogWarning("{@RequestName} {@Email}", "Requested removal of data for the email which is not in the system", email);
                    return 0;
                }

                _logger.LogInformation("{@RequestName} for {@Email}", "Data removed", email);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("{@RequestName} {@Email} with {@Error}", "Failed data removal for ", email, ex);
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
            var updatedUser = await _userRepository.ChangeRoleAsync(_mapper.Map<RoleChangeDto, RoleChange>(request));

            if (updatedUser == null)
            {
                return null;
            }

            return _mapper.Map<User, UserDto>(updatedUser);
        }
        
        public async Task<int> DeleteUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
        
            if (user == null)
            {
                return 0;
            }
            if (!user.Package.ToString().Equals(PackageTypeEnum.Gold.ToString()))
            {
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
                return 0;
            }

            return userIdentityResult + userResult;
        }

        public async Task<bool> BlockUser(string email)
        {
            var identity = await _userIdentityRepository.GetByEmailAsync(email);
            if (identity == null)
                return false;

            identity.BlockAccount();
            await _userIdentityRepository.UpdateAsync(identity);
            _emailService.SendBlockMessage(email);
            return true;
        }
    }
}
