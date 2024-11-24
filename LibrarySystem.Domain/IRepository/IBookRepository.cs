using LibrarySystem.Domain.DTO.Books;
using LibrarySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.IRepository;
public interface IBookRepository:IGenericRepository<Book>
{
    Task<Book?> UpdateAsync(int id, BookRequest request);
}
