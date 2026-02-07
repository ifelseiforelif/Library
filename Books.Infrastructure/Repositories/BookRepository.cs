using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.Application;
using Books.Application.Interfaces;
using Books.Domain.Entities;
using Books.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Books.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _context;
    public BookRepository(LibraryDbContext context)
    {
        _context = context;
    }
    public Task<int?> AddBookAsync(BookEntity book)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<BookEntity>> GetAllBooksAsync()
    {
        return await _context.Books
            //.Include(b=>b.Title)
            //.Include(b=>b.Year)
            .ToListAsync();
    }

    public Task<BookEntity> GetBookById(int id)
    {
        throw new NotImplementedException();
    }
}
