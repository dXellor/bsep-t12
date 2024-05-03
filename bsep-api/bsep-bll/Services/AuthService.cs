using System.Transactions;
using AutoMapper;
using bsep_bll.Contracts;
using bsep_bll.Dtos.Users;
using bsep_dll.Contracts;
using bsep_dll.Data;
using bsep_dll.Models;
using bsep_dll.Models.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace bsep_bll.Services;

public class AuthService: IAuthService
{
    private readonly IUserIdentityRepository _userIdentityRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AuthService> _logger;
    private readonly IConfiguration _configuration;

    public AuthService(IUserIdentityRepository userIdentityRepository, IUserRepository userRepository, IMapper mapper, ILogger<AuthService> logger, IConfiguration configuration)
    {
        _userIdentityRepository = userIdentityRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
        _configuration = configuration;
    }
    
    public async Task<UserDto?> RegisterUser(UserRegistrationDto registrationDto)
    {
        var userEntity = _mapper.Map<UserRegistrationDto, User>(registrationDto);
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            userEntity.Role = UserRoleEnum.Client;
            var newUser = await _userRepository.CreateAsync(userEntity);

            var userIdentity = UserIdentity.CreateUserIdentity(
                registrationDto.Email,
                registrationDto.Password,
                int.Parse(_configuration["Cryptography:Password:SaltLength"]!),
                int.Parse(_configuration["Cryptography:Password:Iterations"]!),
                int.Parse(_configuration["Cryptography:Password:OutputLength"]!));

            await _userIdentityRepository.CreateAsync(userIdentity);

            scope.Complete();
            return _mapper.Map<User, UserDto>(newUser);
        }
        catch (Exception e)
        {
            _logger.LogError("Unable to register user, rollback");
            return null;
        }
    }
}