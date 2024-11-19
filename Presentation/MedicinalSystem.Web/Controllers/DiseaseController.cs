﻿using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Application.Requests.Queries;
using MedicinalSystem.Application.Requests.Commands;

namespace MedicinalSystem.Web.Controllers;

[Route("api/diseases")]
[ApiController]
public class DiseaseController : ControllerBase
{
    private readonly IMediator _mediator;

    public DiseaseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var diseases = await _mediator.Send(new GetDiseasesQuery());

        return Ok(diseases);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var disease = await _mediator.Send(new GetDiseaseByIdQuery(id));

        if (disease is null)
        {
            return NotFound($"Disease with id {id} is not found.");
        }
        
        return Ok(disease);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DiseaseForCreationDto? disease)
    {
        if (disease is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateDiseaseCommand(disease));

        return CreatedAtAction(nameof(Create), disease);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] DiseaseForUpdateDto? disease)
    {
        if (disease is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateDiseaseCommand(disease));

        if (!isEntityFound)
        {
            return NotFound($"Disease with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteDiseaseCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Disease with id {id} is not found.");
        }

        return NoContent();
    }
}
