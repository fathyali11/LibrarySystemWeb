namespace LibrarySystem.Domain.DTO.Reviews;
public record ReviewRequest(string Comment, int Rating, int BookId);
public record ReviewResponse(int Id,string Comment, int Rating, string UserId, int BookId);
