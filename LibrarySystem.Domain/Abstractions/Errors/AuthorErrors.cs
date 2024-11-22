using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Abstractions.Errors;
public static class AuthorErrors
{
    public static readonly Error NotFound = new("Author.NotFound", "This author is not found", StatusCodes.Status400BadRequest);
    public static readonly Error Found = new("Author.IsFound", "This author is found", StatusCodes.Status409Conflict);
}
