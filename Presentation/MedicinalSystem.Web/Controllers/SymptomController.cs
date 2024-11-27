using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Application.Requests.Queries;
using MedicinalSystem.Application.Requests.Commands;
using Microsoft.AspNetCore.Authorization;

namespace MedicinalSystem.Web.Controllers;

[Route("api/symptoms")]
[Authorize]
[ApiController]
public class SymptomController : ControllerBase
{
    private readonly IMediator _mediator;

    public SymptomController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? name = null)
    {
        if (page < 1 || pageSize < 1)
        {
            return BadRequest("Page and pageSize must be greater than zero.");
        }

        var query = new GetSymptomsQuery(page, pageSize, name);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var symptom = await _mediator.Send(new GetSymptomByIdQuery(id));

        if (symptom is null)
        {
            return NotFound($"Symptom with id {id} is not found.");
        }
        
        return Ok(symptom);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SymptomForCreationDto? symptom)
    {
        if (symptom is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateSymptomCommand(symptom));

        return CreatedAtAction(nameof(Create), symptom);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] SymptomForUpdateDto? symptom)
    {
        if (symptom is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateSymptomCommand(symptom));

        if (!isEntityFound)
        {
            return NotFound($"Symptom with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteSymptomCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Symptom with id {id} is not found.");
        }

        return NoContent();
    }
}
