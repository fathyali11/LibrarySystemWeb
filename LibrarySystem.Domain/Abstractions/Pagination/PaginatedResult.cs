using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Domain.Abstractions.Pagination;
public class PaginatedResult<TSource,TResult>
{
    [JsonIgnore]
    public List<TSource> Values { get; init; } = [];
    public List<TResult> Result { get; set; } = [];
    public int TotalPages { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    // Parameterless constructor for deserialization
    public PaginatedResult() { }

    public PaginatedResult(List<TSource> values, int pageNumber, int pageSize, int count)
    {
        Values = values;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling((double)count / pageSize);
    }

    /// <summary>
    /// Creates a paginated result from a source queryable.
    /// </summary>
    /// <remarks>return paginated result and number of pages</remarks>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static PaginatedResult<TSource, TResult> Create(IEnumerable<TSource> source, int pageNumber=1, int pageSize=10)
    {
        int count=source.Count();
        if (pageNumber<1)
            pageNumber = 1;
        if (pageSize<1)
            pageSize = 10;
        var result =source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        return new PaginatedResult<TSource,TResult>(result, pageNumber, pageSize, count);
    }
}
