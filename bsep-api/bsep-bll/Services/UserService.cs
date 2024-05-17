using AutoMapper;
using bsep_bll.Contracts;
using bsep_bll.Dtos.Users;
using bsep_dll.Contracts;
using bsep_dll.Helpers.Pagination;
using bsep_dll.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Principal;

namespace bsep_bll.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserIdentityRepository _userIdentityRepository;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> _logger;
    
    public UserService(IUserRepository userRepository, IUserIdentityRepository userIdentityRepository, ITokenService tokenService, IEmailService emailService, IMapper mapper, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _userIdentityRepository = userIdentityRepository;
        _tokenService = tokenService;
        _emailService = emailService;
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

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var result = await _userRepository.GetByIdAsync(id);
        return _mapper.Map<User, UserDto>(result);
    }

    public Task<UserDto> CreateAsync(UserDto newObject)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> UpdateAsync(UserDto updatedObject)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDto> GetByEmailAsync(string email)
    {
        var result = await _userRepository.GetByEmailAsync(email);
        return _mapper.Map<User, UserDto>(result);
    }

    public async Task<bool> ActivateUser(string email)
    {
        var identity = await _userIdentityRepository.GetByEmailAsync(email);
        if (identity == null)
            return false;
        var activationToken = _tokenService.GenerateActivationToken();

        identity.ActivateAccount();
        identity.SetActivationToken(activationToken.TokenHash, activationToken.Expires);
        await _userIdentityRepository.UpdateAsync(identity);
        _emailService.SendActivationMessage(email, activationToken.Token);
        return true;
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

    public async Task<bool> GenerateOtp(string email)
    {
        var user = await _userIdentityRepository.GetByEmailAsync(email);
        if (user == null)
            return false;
        var otp = _tokenService.GenerateOtp();

        user.SetOtp(otp.OtpCodeHash, otp.Expires);
        await _userIdentityRepository.UpdateAsync(user);
        _emailService.SendOTPMessage(email, otp.OtpCode);
        return true;
    }
}