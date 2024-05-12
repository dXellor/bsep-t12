using Microsoft.EntityFrameworkCore;

namespace bsep_dll.Helpers.Pagination;

public class PagedList<T> : List<T>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PagedList(){}
    
    private PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        CurrentPage = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);
    }

    public static async Task<PagedList<T>> GetAsPagedList(IQueryable<T> source, int pageNumber, int pageSize){
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedList<T>(items, items.Count, pageNumber, pageSize);
    }
    
    public static PagedList<T> ConvertToDtoPagedList(IEnumerable<T> source, int pageNumber, int pageSize){
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        return new PagedList<T>(items, items.Count(), pageNumber, pageSize);
    }

    public object GetMetadata()
    {
        return new  
        {
            TotalCount,
            PageSize,
            CurrentPage,
            TotalPages,
            HasNext,
            HasPrevious
        };
    }
}