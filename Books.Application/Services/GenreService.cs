using AutoMapper;
using Books.Application.DTOs.BookDTOs;
using Books.Application.DTOs.GenreDTOs;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _repository;
    private readonly IMapper _mapper;
    public GenreService(IGenreRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }   
    public async Task<int?> CreateGenreAsync(GenreCreateDto dto)
    {
        var entity = _mapper.Map<GenreEntity>(dto);
        return await _repository.AddGenreAsync(entity);
    }

    public async Task<ICollection<GenreReadDto>> GetAllGenresAsync()
    {
        var entities = await _repository.GetAllGenreAsync();
        return _mapper.Map<ICollection<GenreReadDto>>(entities);
    }

    public async Task<GenreReadDto?> GetGenreByIdAsync(int id)
    {
        var entity = await _repository.GetGenreByIdAsync(id);
        var dto = _mapper.Map<GenreReadDto>(entity);
        return dto;
    }
}
