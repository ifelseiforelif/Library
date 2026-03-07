using Books.Application.DTOs.AuthorDTOs;
using Books.Application.DTOs.GenreDTOs;
using Books.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers;

[ApiController]
[Route("api/[controller]")] //https://localhost:PORT/api/genre
public class GenreController(IGenreService _service) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var genres = await _service.GetAllGenresAsync();
        return Ok(genres);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGenreById([FromRoute] int id)
    {
        var genre = await _service.GetGenreByIdAsync(id);
        return Ok(genre);
    }

   
    [HttpPost]
    public async Task<IActionResult> AddGenre([FromBody] GenreCreateDto genreDto)
    {
        int? id = await _service.CreateGenreAsync(genreDto);
        if (id != null)
        {
            return CreatedAtAction(nameof(GetGenreById), new { id }, id);
        }
        else
        {
            return BadRequest();
        }
    }
}
