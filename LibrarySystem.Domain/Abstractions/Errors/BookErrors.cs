

using Microsoft.AspNetCore.Http;

namespace LibrarySystem.Domain.Abstractions.Errors;
public static class BookErrors
{
    public static readonly Error NotFound = new("Book.NotFound", "This book is not found", StatusCodes.Status400BadRequest);
    public static readonly Error Found = new("Book.IsFound", "This book is found", StatusCodes.Status409Conflict);
}
