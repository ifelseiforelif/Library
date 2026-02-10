using Books.Application.DTOs.BookDTOs;
using Books.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers;

[ApiController]
[Route("api/[controller]")] //https://localhost:PORT/api/books
public class BooksController(IBookRepository _bookRepository):ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _bookRepository.GetAllBooksAsync();
        var result = books.Select(book => new BookReadDto(book));
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById([FromRoute] int id)
    {
        var book = await _bookRepository.GetBookById(id);
        var result = new BookReadDto(book);
        return Ok(result);
    }


}
