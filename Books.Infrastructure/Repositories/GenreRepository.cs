using Books.Application.Interfaces.Repositories;
using Books.Domain.Entities;
using Books.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly LibraryDbContext _context;
    public GenreRepository(LibraryDbContext context)
    {
        _context = context;
    }
    public async Task<int?> AddGenreAsync(GenreEntity genre)
    {
        await _context.Genres.AddAsync(genre);
        await _context.SaveChangesAsync();
        return genre.Id;
    }

    public async Task<ICollection<GenreEntity>> GetAllGenreAsync()
    {
       return await _context.Genres.Include(g => g.Books).ToListAsync();
    }

    public async Task<GenreEntity?> GetGenreByIdAsync(int id)
    {
       return await _context.Genres.Include(g=>g.Books).SingleOrDefaultAsync(g => g.Id == id);
    }
}
