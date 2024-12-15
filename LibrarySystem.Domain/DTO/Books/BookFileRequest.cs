using Microsoft.AspNetCore.Http;

namespace LibrarySystem.Domain.DTO.Books;
public record BookFileRequest(IFormFile Document);