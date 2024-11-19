using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Application.Requests.Queries;
using MedicinalSystem.Application.Requests.Commands;

namespace MedicinalSystem.Web.Controllers;

[Route("api/manufacturers")]
[ApiController]
public class ManufacturerController : ControllerBase
{
    private readonly IMediator _mediator;

    public ManufacturerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var manufacturers = await _mediator.Send(new GetManufacturersQuery());

        return Ok(manufacturers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var manufacturer = await _mediator.Send(new GetManufacturerByIdQuery(id));

        if (manufacturer is null)
        {
            return NotFound($"Manufacturer with id {id} is not found.");
        }
        
        return Ok(manufacturer);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ManufacturerForCreationDto? manufacturer)
    {
        if (manufacturer is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateManufacturerCommand(manufacturer));

        return CreatedAtAction(nameof(Create), manufacturer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ManufacturerForUpdateDto? manufacturer)
    {
        if (manufacturer is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateManufacturerCommand(manufacturer));

        if (!isEntityFound)
        {
            return NotFound($"Manufacturer with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteManufacturerCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Manufacturer with id {id} is not found.");
        }

        return NoContent();
    }
}
