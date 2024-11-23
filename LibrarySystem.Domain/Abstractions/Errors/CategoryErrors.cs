using Microsoft.AspNetCore.Http;
namespace LibrarySystem.Domain.Abstractions.Errors;
public static class CategoryErrors
{
    public static readonly Error NotFound = new("Category.NotFound", "This category is not found", StatusCodes.Status400BadRequest);
    public static readonly Error Found = new("Category.IsFound", "This category is found", StatusCodes.Status409Conflict);
    public static readonly Error Empty = new("Category.IsEmpty", "This category is Empty", StatusCodes.Status400BadRequest);
}

