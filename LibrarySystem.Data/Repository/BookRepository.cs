using AutoMapper;
using LibrarySystem.Data.Data;
using LibrarySystem.Domain.DTO.Books;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.IRepository;
using Microsoft.EntityFrameworkCore;


namespace LibrarySystem.Data.Repository;
public class BookRepository(ApplicationDbContext context, IMapper mapper) : GenericRepository<Book>(context), IBookRepository
{
    private readonly IMapper _mapper=mapper;
    private readonly ApplicationDbContext _context = context;
    public async Task<Book?> UpdateAsync(int id, CreateBookRequest request)
    {
        var book=await GetByIdAsync(id);
        if(book == null)
            return null;
        _mapper.Map(request, book);
        return book;
    }
    public async Task ReturnOne(int bookId, CancellationToken cancellationToken = default)
    {
        await _context.Books
            .Where(x=>x.Id == bookId)
            .ExecuteUpdateAsync(x=>x.SetProperty(b=>b.Quantity, b => b.Quantity + 1), 
            cancellationToken);
    }
}
