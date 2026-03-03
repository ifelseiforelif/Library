using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.DTOs.CountryDTOs;

public class CountryReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
