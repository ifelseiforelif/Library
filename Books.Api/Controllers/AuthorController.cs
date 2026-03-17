using Books.Application.DTOs.AuthorDTOs;
using Books.Application.DTOs.BookDTOs;
using Books.Application.Interfaces.Services;
using Books.Domain.Entities;
using Books.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController(IAuthorService _authorService, IQueueService _queue) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById([FromRoute] int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            return Ok(author);
        }

        //[Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorCreateDto authorDto)
        {
            await _queue.PublishAsync("Authors", authorDto);
            int? id = await _authorService.CreateAuthorAsync(authorDto);
            if (id != null)
            {
                return CreatedAtAction(nameof(GetAuthorById), new { id }, id);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
