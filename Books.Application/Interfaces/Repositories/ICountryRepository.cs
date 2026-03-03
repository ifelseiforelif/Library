using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Repositories;

public interface ICountryRepository
{
    Task<ICollection<CountryEntity?>> GetAllContriesAsync();
    Task<int?> AddCountryAsync(CountryEntity author);
}
