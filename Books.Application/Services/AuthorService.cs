using AutoMapper;
using Books.Application.DTOs.AuthorDTOs;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IMapper _mapper;
    private readonly IAuthorRepository _authorRepository;
    private readonly ICachingService _cacheService;
    public AuthorService(IMapper mapper, IAuthorRepository authorRepository, ICachingService cacheService)
    {
        _mapper = mapper;
        _authorRepository = authorRepository;
        _cacheService = cacheService;
    }
    public async Task<int?> CreateAuthorAsync(AuthorCreateDto dto) 
    {
        var authorEntity = _mapper.Map<AuthorEntity>(dto);
        return await _authorRepository.AddAuthorAsync(authorEntity);
    }

    public async Task<ICollection<AuthorReadDto>> GetAllAuthorsAsync()
    {
        var cache = await _cacheService.GetAsync<ICollection<AuthorReadDto>>("Authors");
        if (cache == null)
        {
            var authors = await _authorRepository.GetAllAuthorsAsync();
            cache = _mapper.Map<ICollection<AuthorReadDto>>(authors);
            await _cacheService.SetAsync("Authors", cache, null);
           
        }
        return cache;

    }

    public async Task<AuthorReadDto?> GetAuthorByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Id must be greater than or equal to 5.", nameof(id));
        return _mapper.Map<AuthorReadDto>(await _authorRepository.GetAuthorByIdAsync(id));
    }
}
