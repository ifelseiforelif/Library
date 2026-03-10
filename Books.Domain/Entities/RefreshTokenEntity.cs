using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Domain.Entities;

public class RefreshTokenEntity
{
    public int Id { get; set; }

    public string Token { get; set; } = string.Empty;

    public DateTime Expires { get; set; }

    public bool IsRevoked { get; set; } // true - значить токен відкликано

    public DateTime Created { get; set; }
    public string IpAddress { get; set; } = string.Empty;

    public Guid UserId { get; set; }
    public UserEntity User { get; set; } = null!;
}
