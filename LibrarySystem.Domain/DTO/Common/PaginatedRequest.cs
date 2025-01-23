namespace LibrarySystem.Domain.DTO.Common;
public class PaginatedRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
    public string? SortTerm { get; set; }
    public string SortBy { get; set; } = "ASC";
}
