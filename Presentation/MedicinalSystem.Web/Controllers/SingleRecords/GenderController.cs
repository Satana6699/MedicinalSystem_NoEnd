using Microsoft.AspNetCore.Mvc;
using MediatR;
using MedicinalSystem.Application.Requests.Queries.Genders;
using MedicinalSystem.Application.Requests.Commands.Genders;
using Microsoft.AspNetCore.Authorization;
using MedicinalSystem.Application.Dtos.Genders;

namespace MedicinalSystem.Web.Controllers.SingleRecords;

[Route("api/genders")]
[Authorize]
[ApiController]
public class GenderController : ControllerBase
{
    private readonly IMediator _mediator;

    public GenderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? name = null)
    {
        var genders = await _mediator.Send(new GetGendersQuery(page, pageSize, name));

        return Ok(genders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var gender = await _mediator.Send(new GetGenderByIdQuery(id));

        if (gender is null)
        {
            return NotFound($"Gender with id {id} is not found.");
        }

        return Ok(gender);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] GenderForCreationDto? gender)
    {
        if (gender is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateGenderCommand(gender));

        return CreatedAtAction(nameof(Create), gender);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] GenderForUpdateDto? gender)
    {
        if (gender is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateGenderCommand(gender));

        if (!isEntityFound)
        {
            return NotFound($"Gender with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteGenderCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Gender with id {id} is not found.");
        }

        return NoContent();
    }
}
