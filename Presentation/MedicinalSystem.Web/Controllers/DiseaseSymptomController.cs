using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Application.Requests.Queries;
using MedicinalSystem.Application.Requests.Commands;
using Bogus.DataSets;

namespace MedicinalSystem.Web.Controllers;

[Route("api/diseaseSymptoms")]
[ApiController]
public class DiseaseSymptomController : ControllerBase
{
    private readonly IMediator _mediator;

    public DiseaseSymptomController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? nameDisease = null, [FromQuery] string? nameSymptom = null)
    {
        if (page < 1 || pageSize < 1)
        {
            return BadRequest("Page and pageSize must be greater than zero.");
        }
        var diseaseSymptoms = await _mediator.Send(new GetDiseaseSymptomsQuery(page, pageSize, nameDisease, nameSymptom));

        var query = new GetDiseaseSymptomsQuery(page, pageSize, nameDisease, nameSymptom);
        var result = await _mediator.Send(query);

        return Ok(diseaseSymptoms);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var diseaseSymptom = await _mediator.Send(new GetDiseaseSymptomByIdQuery(id));

        if (diseaseSymptom is null)
        {
            return NotFound($"DiseaseSymptom with id {id} is not found.");
        }
        
        return Ok(diseaseSymptom);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DiseaseSymptomForCreationDto? diseaseSymptom)
    {
        if (diseaseSymptom is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateDiseaseSymptomCommand(diseaseSymptom));

        return CreatedAtAction(nameof(Create), diseaseSymptom);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] DiseaseSymptomForUpdateDto? diseaseSymptom)
    {
        if (diseaseSymptom is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateDiseaseSymptomCommand(diseaseSymptom));

        if (!isEntityFound)
        {
            return NotFound($"DiseaseSymptom with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteDiseaseSymptomCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"DiseaseSymptom with id {id} is not found.");
        }

        return NoContent();
    }
}
