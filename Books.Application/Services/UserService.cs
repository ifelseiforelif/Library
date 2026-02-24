using AutoMapper;
using Books.Application.DTOs.UserDTOs;
using Books.Application.Interfaces.Helpers;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IJwtService _jwtService;
    private readonly IHashHelper _hashHelper;

    public UserService(IUserRepository userRepository, IMapper mapper, IJwtService jwtService, IHashHelper hashHelper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtService = jwtService;
        _hashHelper = hashHelper;
    }
    public async Task<string> CreateUserAsync(UserCreateDto dto)
    {
    
        var entity = _mapper.Map<UserEntity>(dto);
        dto.Email = dto.Email.Trim();
        return await _userRepository.AddUserAsync(entity, dto.Password);
    }

    public async Task<ICollection<UserReadDto>> GetAllUserAsync()
    {
        var users = await _userRepository.GetAllUserAsync();
        return _mapper.Map<ICollection<UserReadDto>>(users);
    }
    public async Task<UserReadDto?> GetByEmailUserAsync(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null) return null;

        return _mapper.Map<UserReadDto>(user);
    }

    public async Task<string> LoginAsync(UserLoginDto dto)
    {
        // Чи існує користувач з таким email
        dto.Email = dto.Email.Trim();
        var user = await _userRepository.GetUserByEmailAsync(dto.Email);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Невірний email або пароль");
        }

        // Перевірка валідності пароля
        if(!_hashHelper.IsValidPassword(dto.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Неверный логин или пароль");

        }
        if(!user.IsActive)
        {
            throw new UnauthorizedAccessException("Користувача заблоковано");

        }

        // Генеруємо JWT токен для авторизованого користувача
        var token = _jwtService.GenerateAccessToken(dto, user.Role.ToString());

        // Повертаємо токен
        return token;
    }
}
