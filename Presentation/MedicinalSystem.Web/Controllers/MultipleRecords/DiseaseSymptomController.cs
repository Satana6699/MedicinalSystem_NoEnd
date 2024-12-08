using Microsoft.AspNetCore.Mvc;
using MediatR;
using MedicinalSystem.Application.Requests.Queries.Diseases;
using MedicinalSystem.Application.Requests.Commands.Diseases;
using MedicinalSystem.Application.Requests.Queries.Symptoms;
using MedicinalSystem.Application.Requests.Commands.Symptoms;
using MedicinalSystem.Application.Requests.Queries.DiseaseSymptoms;
using MedicinalSystem.Application.Requests.Commands.DiseaseSymptoms;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using MedicinalSystem.Application.Dtos.DiseaseSymptoms;

namespace MedicinalSystem.Web.Controllers.MultipleRecords;

[Route("api/diseaseSymptoms")]
[Authorize]
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

        var result = await _mediator.Send(new GetDiseaseSymptomsQuery(page, pageSize, nameDisease, nameSymptom));

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
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
    [Authorize(Roles = "Admin")]
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

        var diseaseSymptomDto = await _mediator.Send(new GetDiseaseSymptomByIdQuery(diseaseSymptom.Id));

        if (diseaseSymptomDto is null)
        {
            return NotFound($"DiseaseSymptom with id {id} is not found.");
        }

        return Ok(diseaseSymptomDto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteDiseaseSymptomCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"DiseaseSymptom with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpGet("symptoms")]
    public async Task<IActionResult> GetSymptoms([FromQuery] string? name = null)
    {
        var result = await _mediator.Send(new GetSymptomsAllQuery(name));

        return Ok(result);
    }

    [HttpGet("diseases")]
    public async Task<IActionResult> GetDiseases([FromQuery] string? name = null)
    {
        var result = await _mediator.Send(new GetDiseasesAllQuery(name));

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var diseaseSymptom = await _mediator.Send(new GetDiseaseSymptomByIdQuery(id));

        if (diseaseSymptom is null)
        {
            return NotFound($"DiseaseSymptom with id {id} is not found.");
        }

        return Ok(diseaseSymptom);
    }
}
