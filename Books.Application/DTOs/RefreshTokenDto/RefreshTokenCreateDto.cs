using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.DTOs.RefreshTokenDto;

public class RefreshTokenCreateDto
{
    public string Token { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public Guid UserId { get; set; }
}

