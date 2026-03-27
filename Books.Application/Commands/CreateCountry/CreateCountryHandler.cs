using Books.Application.Commands.CreateBook;
using Books.Application.Interfaces.Repositories;
using Books.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Commands.CreateCountry;

public class CreateCountryHandler : IRequestHandler<CreateCountryCommand, int?>
{
    private readonly ICountryRepository _repository;
    public CreateCountryHandler(ICountryRepository repository)
    {
            _repository = repository;
    }
    public async Task<int?> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        var country = new CountryEntity();
        country.Name = request.name;  
        var result = await _repository.AddCountryAsync(country);
        return result;
    }
}
