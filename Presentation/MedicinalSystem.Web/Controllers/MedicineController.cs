using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Application.Requests.Queries;
using MedicinalSystem.Application.Requests.Commands;

namespace MedicinalSystem.Web.Controllers;

[Route("api/medicines")]
[ApiController]
public class MedicineController : ControllerBase
{
    private readonly IMediator _mediator;

    public MedicineController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var medicines = await _mediator.Send(new GetMedicinesQuery());

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
