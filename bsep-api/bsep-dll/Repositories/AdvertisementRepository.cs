using bsep_dll.Contracts;
using bsep_dll.Data;
using bsep_dll.Helpers.Pagination;
using bsep_dll.Helpers.QueryParameters;
using bsep_dll.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace bsep_dll.Repositories;

public class AdvertisementRepository : IAdvertisementRepository
{
    private readonly ILogger<AdvertisementRepository> _logger;
    private readonly DataContext _dataContext;
    private readonly IConfiguration _configuration;
    private readonly DbSet<Advertisement> _advertisements;
    
    public AdvertisementRepository(ILogger<AdvertisementRepository> logger, DataContext dataContext, IConfiguration configuration)
    {
        _logger = logger;
        _dataContext = dataContext;
        _advertisements = dataContext.Advertisements;
        _configuration = configuration;
    }
    public async Task<PagedList<Advertisement>> GetAllAsync(QueryPageParameters queryParameters)
    {
        try
        {
            var adQueryParameters = (AdvertisementQueryParameters)queryParameters;
            var query = _advertisements
                .AsNoTracking()
                .Where(a => DateTime.UtcNow.CompareTo(a.End) <= 0)
                .OrderBy(a => a.Start);


            var pagedList = await PagedList<Advertisement>.GetAsPagedList(query, adQueryParameters.PageNumber, adQueryParameters.PageSize);
            return pagedList;
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            throw;
        }
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
        var advertisement = _dataContext.Advertisements.Find(id);
        if (advertisement != null)
        {
            _dataContext.Advertisements.Remove(advertisement);
            _dataContext.SaveChanges();
            return Task.FromResult(1); 
        }
        return Task.FromResult(0);
    }
    
    public async Task<List<Advertisement>> GetAdvertisementsByUserIdAsync(int userId)
    {
        return await _dataContext.Advertisements
            .Where(ad => ad.UserId == userId)
            .ToListAsync();
    }
}