namespace LibrarySystem.Services.Services.BorrowedBooks;
/// <include file='ExternalServicesDocs\BorrowedBooksDocs.xml' path='/docs/members[@name="borrowedBookServices"]/BorrowedBookServices'/>
public class BorrowedBookServices(ApplicationDbContext context,
    IUnitOfWork unitOfWork,HybridCache hybridCache): BorrowedBookRepository(context), IBorrowedBookServices
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HybridCache _hybridCache = hybridCache;
    /// <include file='ExternalServicesDocs\BorrowedBooksDocs.xml' path='/docs/members[@name="borrowedBookServices"]/ReturnBookAsync'/>
    public async Task<OneOf<bool,Error>> ReturnBookAsync(string userId,int borrowedBookId,CancellationToken cancellationToken=default)
    {
        if(borrowedBookId <= 0)
            return BookErrors.NotFound;
        var IsExits = await _unitOfWork.BorrowedBookRepository.IsExits(x=>x.Id==borrowedBookId);
        if(!IsExits)
            return BookErrors.NotFound;

        int bookId= await _unitOfWork.BorrowedBookRepository.ReturningAndGetBookId(borrowedBookId);


        await _unitOfWork.FineRepository.PayingOne(userId, borrowedBookId, cancellationToken);
        await _unitOfWork.BookRepository.ReturnOne(bookId, cancellationToken);
        await _hybridCache.RemoveAsync(GeneralConsts.AllBooksCachedKey, cancellationToken);
        await _hybridCache.RemoveAsync(GeneralConsts.AllAvailableBooksCachedKey, cancellationToken);
        return true;
    }
    /// <include file='ExternalServicesDocs\BorrowedBooksDocs.xml' path='/docs/members[@name="borrowedBookServices"]/GetAllBorrowedBooksAsync'/>
    public async Task<List<borrowedBookResponse>> GetAllBorrowedBooksAsync(CancellationToken cancellationToken=default)
    {
        var result = await _unitOfWork.BorrowedBookRepository.GetAllWithUserAndBookForDisplay(cancellationToken);
        return result;
    }
}
