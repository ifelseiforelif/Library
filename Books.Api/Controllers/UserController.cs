using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController:ControllerBase
{
    [HttpGet("{password}")]
    public IActionResult GetPasswordHash([FromRoute] string password)
    {
        string hash = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        password= "kdk";

        bool isValid = BCrypt.Net.BCrypt.EnhancedVerify(password, hash);

        return Ok(new {hash=hash, isValid=isValid});
    }
}
