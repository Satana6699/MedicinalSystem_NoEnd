using Microsoft.AspNetCore.Mvc;
using MediatR;
using MedicinalSystem.Application.Requests.Queries.Medicines;
using MedicinalSystem.Application.Requests.Commands.Medicines;
using Microsoft.AspNetCore.Authorization;
using MedicinalSystem.Application.Dtos.Medicines;

namespace MedicinalSystem.Web.Controllers;

[Route("api/medicines")]
[Authorize]
[ApiController]
public class MedicineController : ControllerBase
{
    private readonly IMediator _mediator;

    public MedicineController(IMediator mediator)
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
        var medicines = await _mediator.Send(new GetMedicinesQuery(page, pageSize, name));

        return Ok(medicines);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var medicine = await _mediator.Send(new GetMedicineByIdQuery(id));

        if (medicine is null)
        {
            return NotFound($"Medicine with id {id} is not found.");
        }
        
        return Ok(medicine);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MedicineForCreationDto? medicine)
    {
        if (medicine is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateMedicineCommand(medicine));

        return CreatedAtAction(nameof(Create), medicine);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] MedicineForUpdateDto? medicine)
    {
        if (medicine is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateMedicineCommand(medicine));

        if (!isEntityFound)
        {
            return NotFound($"Medicine with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteMedicineCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Medicine with id {id} is not found.");
        }

        return NoContent();
    }
}
