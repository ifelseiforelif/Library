using AutoMapper;
using Books.Application.DTOs.CountryDTOs;
using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Mapping;

public class CountryProfile: Profile
{
    public CountryProfile()
    {
        CreateMap<CountryEntity, CountryReadDto>();
    }
}
