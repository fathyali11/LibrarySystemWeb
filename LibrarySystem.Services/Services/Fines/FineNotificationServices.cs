using Stripe.V2;

namespace LibrarySystem.Services.Services.Fines;
public class FineNotificationServices(ApplicationDbContext context,IUnitOfWork unitOfWork):FineRepository(context),IFineNotificationServices
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;

    public async Task AddFine()
    {
        var borrowedBooks = await _unitOfWork.BorrowedBookRepository
            .GetAllBooksAndUser(x => x.DueDate < DateTime.Now);

        foreach (var borrowedBook in borrowedBooks)
        {
            var fineFromDb=await _unitOfWork.FineRepository
                .ExitsOrNot(x => x.UserId == borrowedBook.UserId && x.BorrowBookId == borrowedBook.BorrowedBookId);
            Fine fine = fineFromDb ??
               new Fine
               {
                   UserId = borrowedBook.UserId,
                   BorrowBookId = borrowedBook.BorrowedBookId
               };
            await _unitOfWork.BorrowedBookRepository.AddToFines(borrowedBook.BorrowedBookId,fine.Id);
            if (fineFromDb is null)
                await _unitOfWork.FineRepository.AddAsync(fine);
            
            fine.TotalAmount += fine.Amount;
            await _unitOfWork.SaveChanges();
        }


    }
}
