using LibrarySystem.Domain.DTO.BorrowBooks;

namespace LibrarySystem.Services.Services.BorrowedBooks;
public interface IBorrowedBookServices:IBorrowedBookRepository
{
    Task<OneOf<bool, Error>> ReturnBookAsync(string userId, int borrowedBookId, CancellationToken cancellationToken = default);
    Task<List<borrowedBookResponse>> GetAllBorrowedBooksAsync(CancellationToken cancellationToken = default);
}
