using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Application.Requests.Queries;
using MedicinalSystem.Application.Requests.Commands;

namespace MedicinalSystem.Web.Controllers;

[Route("api/familyMembers")]
[ApiController]
public class FamilyMemberController : ControllerBase
{
    private readonly IMediator _mediator;

    public FamilyMemberController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var familyMembers = await _mediator.Send(new GetFamilyMembersQuery());

        return Ok(familyMembers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var familyMember = await _mediator.Send(new GetFamilyMemberByIdQuery(id));

        if (familyMember is null)
        {
            return NotFound($"FamilyMember with id {id} is not found.");
        }
        
        return Ok(familyMember);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] FamilyMemberForCreationDto? familyMember)
    {
        if (familyMember is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateFamilyMemberCommand(familyMember));

        return CreatedAtAction(nameof(Create), familyMember);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] FamilyMemberForUpdateDto? familyMember)
    {
        if (familyMember is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateFamilyMemberCommand(familyMember));

        if (!isEntityFound)
        {
            return NotFound($"FamilyMember with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteFamilyMemberCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"FamilyMember with id {id} is not found.");
        }

        return NoContent();
    }
}
