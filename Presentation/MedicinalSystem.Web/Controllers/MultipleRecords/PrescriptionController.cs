using Microsoft.AspNetCore.Mvc;
using MediatR;
using MedicinalSystem.Application.Requests.Queries.Prescriptions;
using MedicinalSystem.Application.Requests.Commands.Prescriptions;
using Microsoft.AspNetCore.Authorization;
using MedicinalSystem.Application.Dtos.Prescriptions;

namespace MedicinalSystem.Web.Controllers.MultipleRecords;

[Route("api/prescriptions")]
[Authorize]
[ApiController]
public class PrescriptionController : ControllerBase
{
    private readonly IMediator _mediator;

    public PrescriptionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? name = null)
    {
        var prescriptions = await _mediator.Send(new GetPrescriptionsQuery(page, pageSize, name));

        return Ok(prescriptions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var prescription = await _mediator.Send(new GetPrescriptionByIdQuery(id));

        if (prescription is null)
        {
            return NotFound($"Prescription with id {id} is not found.");
        }

        return Ok(prescription);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PrescriptionForCreationDto? prescription)
    {
        if (prescription is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreatePrescriptionCommand(prescription));

        return CreatedAtAction(nameof(Create), prescription);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] PrescriptionForUpdateDto? prescription)
    {
        if (prescription is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdatePrescriptionCommand(prescription));

        if (!isEntityFound)
        {
            return NotFound($"Prescription with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeletePrescriptionCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Prescription with id {id} is not found.");
        }

        return NoContent();
    }
}
