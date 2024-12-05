using Microsoft.AspNetCore.Mvc;
using MediatR;
using MedicinalSystem.Application.Requests.Queries.MedicinePrices;
using MedicinalSystem.Application.Requests.Commands.MedicinePrices;
using Microsoft.AspNetCore.Authorization;
using MedicinalSystem.Application.Dtos.MedicinePrices;

namespace MedicinalSystem.Web.Controllers.MultipleRecords;

[Route("api/medicinePrices")]
[Authorize]
[ApiController]
public class MedicinePriceController : ControllerBase
{
    private readonly IMediator _mediator;

    public MedicinePriceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? name = null)
    {
        var medicinePrices = await _mediator.Send(new GetMedicinePricesQuery(page, pageSize, name));

        return Ok(medicinePrices);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var medicinePrice = await _mediator.Send(new GetMedicinePriceByIdQuery(id));

        if (medicinePrice is null)
        {
            return NotFound($"MedicinePrice with id {id} is not found.");
        }

        return Ok(medicinePrice);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MedicinePriceForCreationDto? medicinePrice)
    {
        if (medicinePrice is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateMedicinePriceCommand(medicinePrice));

        return CreatedAtAction(nameof(Create), medicinePrice);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] MedicinePriceForUpdateDto? medicinePrice)
    {
        if (medicinePrice is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateMedicinePriceCommand(medicinePrice));

        if (!isEntityFound)
        {
            return NotFound($"MedicinePrice with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteMedicinePriceCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"MedicinePrice with id {id} is not found.");
        }

        return NoContent();
    }
}
