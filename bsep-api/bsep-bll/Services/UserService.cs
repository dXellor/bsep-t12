using AutoMapper;
using bsep_bll.Contracts;
using bsep_bll.Dtos.Users;
using bsep_dll.Contracts;
using bsep_dll.Helpers.Pagination;
using bsep_dll.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace bsep_bll.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> _logger;
    
    public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
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
}