using bsep_dll.Contracts;
using bsep_dll.Data;
using bsep_dll.Helpers.Pagination;
using bsep_dll.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace bsep_dll.Repositories;

public class AdvertisementRepository : IAdvertisementRepository
{
    private readonly ILogger<AdvertisementRepository> _logger;
    private readonly DataContext _dataContext;
    private readonly DbSet<Advertisement> _advertisements;
    
    public AdvertisementRepository(ILogger<AdvertisementRepository> logger, DataContext dataContext)
    {
        _logger = logger;
        _dataContext = dataContext;
        _advertisements = dataContext.Advertisements;
    }
    public Task<PagedList<Advertisement>> GetAllAsync(QueryPageParameters queryParameters)
    {
        throw new NotImplementedException();
    }

    public Task<Advertisement> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Advertisement> CreateAsync(Advertisement newObject)
    {
        throw new NotImplementedException();
    }

    public Task<Advertisement> UpdateAsync(Advertisement updatedObject)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}