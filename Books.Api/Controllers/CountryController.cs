using Books.Application.Commands.CreateBook;
using Books.Application.DTOs.CountryDTOs;
using Books.Application.Queries.GetAllCountry;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers;

[ApiController]
[Route("api/[controller]")] //https://localhost:PORT/api/country
public class CountryController(IMediator _mediator):ControllerBase
{
    [HttpGet]
    public async  Task<IActionResult> GetAllCountries()
    {
        var result = await _mediator.Send(new GetAllCountriesQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCountry([FromBody] CountryCreateDto dto)
    {
        var result = await _mediator.Send(new CreateCountryCommand(dto.Name));
        return Ok(result);
    }
}
