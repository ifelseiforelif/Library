using Books.Application;
using Books.Application.DTOs.BookDTOs;
using Books.Application.Interfaces;
using Books.Domain.Entities;
using Books.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _context;
    public BookRepository(LibraryDbContext context)
    {
        _context = context;
    }
    public async Task<int?> AddBookAsync(BookCreateDto bookDto)
    {
        var authors = await _context.Authors.Where(a => bookDto.AuthorsId.Contains(a.Id)).ToListAsync();
        if (authors.Count != bookDto.AuthorsId.Count)
            throw new Exception("Some authors not found");
        var book = new BookEntity
        {
            Title = bookDto.Title,
            Year = bookDto.Year,
            GenreId = bookDto.GenreId,
            Authors = authors
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book.Id;
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
