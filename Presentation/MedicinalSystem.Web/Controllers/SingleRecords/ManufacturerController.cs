using Microsoft.AspNetCore.Mvc;
using MediatR;
using MedicinalSystem.Application.Requests.Queries.Manufacturers;
using MedicinalSystem.Application.Requests.Commands.Manufacturers;
using Microsoft.AspNetCore.Authorization;
using MedicinalSystem.Application.Dtos.Manufacturers;

namespace MedicinalSystem.Web.Controllers.SingleRecords;

[Route("api/manufacturers")]
[Authorize]
[ApiController]
public class ManufacturerController : ControllerBase
{
    private readonly IMediator _mediator;

    public ManufacturerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? name = null)
    {
        var manufacturers = await _mediator.Send(new GetManufacturersQuery(page, pageSize, name));

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
