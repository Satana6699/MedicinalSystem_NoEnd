using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Application.Requests.Queries;
using MedicinalSystem.Application.Requests.Commands;

namespace MedicinalSystem.Web.Controllers;

[Route("api/treatments")]
[ApiController]
public class TreatmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public TreatmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var treatments = await _mediator.Send(new GetTreatmentsQuery());

        return Ok(treatments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var treatment = await _mediator.Send(new GetTreatmentByIdQuery(id));

        if (treatment is null)
        {
            return NotFound($"Treatment with id {id} is not found.");
        }
        
        return Ok(treatment);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TreatmentForCreationDto? treatment)
    {
        if (treatment is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateTreatmentCommand(treatment));

        return CreatedAtAction(nameof(Create), treatment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] TreatmentForUpdateDto? treatment)
    {
        if (treatment is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateTreatmentCommand(treatment));

        if (!isEntityFound)
        {
            return NotFound($"Treatment with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteTreatmentCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Treatment with id {id} is not found.");
        }

        return NoContent();
    }
}
