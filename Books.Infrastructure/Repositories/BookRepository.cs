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
    public async Task<int?> AddBookAsync(BookEntity book)
    {
        _context.Books.Add(book);
        return await _context.SaveChangesAsync();
    }

    public async Task<ICollection<BookEntity>> GetAllBooksAsync()
    {
        return await _context.Books
            .Include(b=>b.Authors)
            .ToListAsync();
    }

    public async Task<BookEntity> GetBookById(int id)
    {
        return await _context.Books.Include(b => b.Authors).SingleOrDefaultAsync(b => b.Id == id);
    }
}
