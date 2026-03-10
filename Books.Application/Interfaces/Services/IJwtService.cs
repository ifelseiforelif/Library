using Books.Application.DTOs.RefreshTokenDto;
using Books.Application.DTOs.UserDTOs;
using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Services;

public interface IJwtService
{
    string GenerateAccessToken(UserLoginDto userLoginDto, string role);
    Task<RefreshTokenReadDto> GenerateRefreshToken(UserLoginDto userLoginDto, string ipAddress);
}
