using Books.Api.ExceptionHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Books.Api.Controllers
{
    public class User
    {
        [Range(1,100, ErrorMessage ="Indentificator fail (1-100)")]
        public int Id { get; set; }

        [EmailAddress(ErrorMessage ="Email is wrong")]
        public string Email { get; set; } = null!;
    }
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateData([FromBody] User user)
        {
            return Ok();

        }

        [HttpGet("{id}")]
        public IActionResult GetItemById([FromRoute] int id)
        {
        
                return Ok();
          
        }
    }
}
