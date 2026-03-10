using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.DTOs.RefreshTokenDto;

public class RefreshTokenReadDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; }
    public Guid UserId { get; set; }

}
