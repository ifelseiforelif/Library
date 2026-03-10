using Books.Application.DTOs.RefreshTokenDto;
using Books.Application.Interfaces.Repositories;
using Books.Domain.Entities;
using Books.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly LibraryDbContext _context;
    public RefreshTokenRepository(LibraryDbContext context)
    {
        _context = context;
    }
    public async Task<RefreshTokenEntity> AddToken(RefreshTokenCreateDto dto)
    {
        var refreshTokenEntity = new RefreshTokenEntity();
        refreshTokenEntity.Token = dto.Token;
        refreshTokenEntity.IpAddress =  dto.IpAddress;
        refreshTokenEntity.UserId = dto.UserId;
        await _context.AddAsync(refreshTokenEntity);
        await _context.SaveChangesAsync();
        return refreshTokenEntity;
    }
}
