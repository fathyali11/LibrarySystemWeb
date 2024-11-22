﻿using AutoMapper;
using LibrarySystem.Data.Data;
using LibrarySystem.Domain.DTO;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.IRepository;


namespace LibrarySystem.Data.Repository;
public class BookRepository(ApplicationDbContext context, IMapper mapper) : GenericRepository<Book>(context), IBookRepository
{
    private readonly IMapper _mapper=mapper;

    public async Task UpdateAsync(int id, BookRequest request)
    {
        var book=await GetByIdAsync(id);
        _mapper.Map(request, book);
    }
}