using Books.Application.DTOs.RefreshTokenDto;
using Books.Application.DTOs.UserDTOs;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using Books.Domain.Entities;
using Books.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IRefreshTokenRepository _repository;
    private readonly IUserRepository _userRepository;

    public JwtService(IOptions<JwtSettings> jwtOptions, IRefreshTokenRepository repository, IUserRepository userRepository)
    {
        _jwtSettings = jwtOptions.Value;
        _repository = repository;
        _userRepository = userRepository;
    }

    public string GenerateAccessToken(UserLoginDto userLoginDto, string role)
    {
        var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, userLoginDto.Email),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var signingKey = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public async Task<RefreshTokenReadDto> GenerateRefreshToken(UserLoginDto userLoginDto,string ipAddress)
    {
        var randomBytes = new byte[64]; // 512 біт

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        var user = await _userRepository.GetUserByEmailAsync(userLoginDto.Email); 

        var entity =  await _repository.AddToken(new RefreshTokenCreateDto()
        {
            Token = Convert.ToBase64String(randomBytes),
            IpAddress = ipAddress,
            UserId = user.Id,

        });
        return new RefreshTokenReadDto()
        {
            Token = entity.Token,
            UserId = entity.UserId,
            Expires = entity.Expires
        };
    }

   
}