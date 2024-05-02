using bsep_dll.Helpers.Pagination;

namespace bsep_dll.Contracts;

public interface ICrudRepository<T> where T : class
{
    public Task<PagedList<T>> GetAllAsync(QueryPageParameters queryParameters);
    public Task<T> GetByIdAsync(int id);
    public Task<T> CreateAsync(T newObject);
    public Task<T> UpdateAsync(T updatedObject);
    public Task<int> DeleteAsync(int id);
}