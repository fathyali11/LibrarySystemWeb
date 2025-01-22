using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Domain.Abstractions.Pagination;
public static class PaginatedResult
{
    /// <summary>
    /// Creates a paginated result from a source queryable.
    /// </summary>
    /// <remarks>return paginated result and number of pages</remarks>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static async Task<(List<T>,int)> Create<T>(this IQueryable<T> source, int pageNumber=1, int pageSize=10)
    {
        if(pageNumber<1)
            pageNumber = 1;
        if (pageSize<10)
            pageSize = 10;
        var result=await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        var totalPages = (int)Math.Ceiling((double)(source.Count()/pageSize));
        return (result, totalPages);
    }
}
