using Books.Application.DTOs.CountryDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Queries.GetAllCountry;

public record GetAllCountriesQuery():IRequest<ICollection<CountryReadDto>>;