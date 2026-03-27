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

public class CountryRepository : ICountryRepository
{
    private readonly LibraryDbContext _context;
    public CountryRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<int?> AddCountryAsync(CountryEntity country)
    {
       await _context.Countries.AddAsync(country);
       await _context.SaveChangesAsync();
        return country.Id;
    }

    public async Task<ICollection<CountryEntity?>> GetAllContriesAsync()
    {
        return await _context.Countries.ToListAsync();
    }
}
