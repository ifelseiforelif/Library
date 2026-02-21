using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Services;

public interface IJwtService
{
    string GenerateAccessToken(UserEntity userEntity);
}
