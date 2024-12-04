﻿using LibrarySystem.Domain.DTO.Books;
using LibrarySystem.Domain.Entities;
namespace LibrarySystem.Domain.IRepository;
public interface IBookRepository:IGenericRepository<Book>
{
    Task<Book?> UpdateAsync(int id, BookRequest request);
    Task<Book?> IsAvailableAsync(int id);
}
