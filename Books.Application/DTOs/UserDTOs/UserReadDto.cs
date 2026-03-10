using Books.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.DTOs.UserDTOs;

public class UserReadDto
{
    public int? Id { get; set; }
    public string Email { get; set; } = String.Empty;
    public string Role { get; set; } = String.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}
