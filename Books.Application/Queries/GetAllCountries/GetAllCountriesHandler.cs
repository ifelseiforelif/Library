using AutoMapper;
using Books.Application.DTOs.CountryDTOs;
using Books.Application.Interfaces.Repositories;
using Books.Application.Queries.GetAllCountry;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Queries.GetAllCountries;

public class GetAllCountriesHandler : IRequestHandler<GetAllCountriesQuery, ICollection<CountryReadDto>>
{
    private readonly ICountryRepository _repository;
    private readonly IMapper _mapper;
    public GetAllCountriesHandler(ICountryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ICollection<CountryReadDto>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllContriesAsync();
        return _mapper.Map<ICollection<CountryReadDto>>(entities);
     
    }
}
