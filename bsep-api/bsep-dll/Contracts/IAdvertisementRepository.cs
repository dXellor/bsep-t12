using bsep_dll.Models;

namespace bsep_dll.Contracts;

public interface IAdvertisementRepository : ICrudRepository<Advertisement>
{
    Task<List<Advertisement>> GetAdvertisementsByUserIdAsync(int userId);
}