using AutoMapper;
using bsep_bll.Contracts;
using bsep_bll.Dtos;
using bsep_dll.Contracts;
using bsep_dll.Helpers.Pagination;
using Microsoft.Extensions.Logging;

namespace bsep_bll.Services;

public class AdvertisementService : IAdvertisementService
{
    private readonly IAdvertisementRepository _adRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AdvertisementService> _logger;

    public AdvertisementService(IAdvertisementRepository adRepository, IMapper mapper, ILogger<AdvertisementService> logger)
    {
        _adRepository = adRepository;
        _mapper = mapper;
        _logger = logger;
    }
    public Task<PagedList<AdvertisementDto>> GetAllAsync(QueryPageParameters queryParameters)
    {
        throw new NotImplementedException();
    }

    public Task<AdvertisementDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<AdvertisementDto> CreateAsync(AdvertisementDto newObject)
    {
        throw new NotImplementedException();
    }

    public Task<AdvertisementDto> UpdateAsync(AdvertisementDto updatedObject)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}