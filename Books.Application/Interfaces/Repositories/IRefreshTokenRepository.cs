using Books.Application.DTOs.RefreshTokenDto;
using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Repositories;

public interface IRefreshTokenRepository
{   
    Task<RefreshTokenEntity> AddToken(RefreshTokenCreateDto dto);
}
