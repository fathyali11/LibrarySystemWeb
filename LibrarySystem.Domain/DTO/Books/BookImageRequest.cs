using Microsoft.AspNetCore.Http;
namespace LibrarySystem.Domain.DTO.Books;
public record BookImageRequest(IFormFile Image);