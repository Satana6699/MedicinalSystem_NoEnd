using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using MedicinalSystem.Application.Requests.Queries.FamilyMembers;
using MedicinalSystem.Application.Requests.Commands.FamilyMembers;
using MedicinalSystem.Web.Controllers.MultipleRecords;
using MedicinalSystem.Application.Dtos.FamilyMembers;

namespace MedicinalSystem.Tests.ControllersTests;

public class FamilyMemberControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly FamilyMemberController _controller;

    public FamilyMemberControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new FamilyMemberController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetById_ExistingFamilyMemberId_ReturnsFamilyMember()
    {
        // Arrange
        var familyMemberId = Guid.NewGuid();
        var familyMember = new FamilyMemberDto { Id = familyMemberId };

        _mediatorMock
            .Setup(m => m.Send(new GetFamilyMemberByIdQuery(familyMemberId), CancellationToken.None))
            .ReturnsAsync(familyMember);

        // Act
        var result = await _controller.GetById(familyMemberId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as FamilyMemberDto).Should().BeEquivalentTo(familyMember);

        _mediatorMock.Verify(m => m.Send(new GetFamilyMemberByIdQuery(familyMemberId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingFamilyMemberId_ReturnsNotFoundResult()
    {
        // Arrange
        var familyMemberId = Guid.NewGuid();
        var familyMember = new FamilyMemberDto { Id = familyMemberId };

        _mediatorMock
            .Setup(m => m.Send(new GetFamilyMemberByIdQuery(familyMemberId), CancellationToken.None))
            .ReturnsAsync((FamilyMemberDto?)null);

        // Act
        var result = await _controller.GetById(familyMemberId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetFamilyMemberByIdQuery(familyMemberId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_FamilyMember_ReturnsFamilyMember()
    {
        // Arrange
        var familyMember = new FamilyMemberForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateFamilyMemberCommand(familyMember), CancellationToken.None));

        // Act
        var result = await _controller.Create(familyMember);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as FamilyMemberForCreationDto).Should().BeEquivalentTo(familyMember);

        _mediatorMock.Verify(m => m.Send(new CreateFamilyMemberCommand(familyMember), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_NullValue_ReturnsBadRequest()
    {
        // Arrange and Act
        var result = await _controller.Create(null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new CreateFamilyMemberCommand(It.IsAny<FamilyMemberForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingFamilyMember_ReturnsNoContentResult()
    {
        // Arrange
        var familyMemberId = Guid.NewGuid();
        var familyMember = new FamilyMemberForUpdateDto { Id = familyMemberId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateFamilyMemberCommand(familyMember), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(familyMemberId, familyMember);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateFamilyMemberCommand(familyMember), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingFamilyMember_ReturnsNotFoundResult()
    {
        // Arrange
        var familyMemberId = Guid.NewGuid();
        var familyMember = new FamilyMemberForUpdateDto { Id = familyMemberId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateFamilyMemberCommand(familyMember), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(familyMemberId, familyMember);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateFamilyMemberCommand(familyMember), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var familyMemberId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(familyMemberId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateFamilyMemberCommand(It.IsAny<FamilyMemberForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingFamilyMemberId_ReturnsNoContentResult()
    {
        // Arrange
        var familyMemberId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteFamilyMemberCommand(familyMemberId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(familyMemberId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteFamilyMemberCommand(familyMemberId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingFamilyMemberId_ReturnsNotFoundResult()
    {
        // Arrange
        var familyMemberId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteFamilyMemberCommand(familyMemberId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(familyMemberId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteFamilyMemberCommand(familyMemberId), CancellationToken.None), Times.Once);
    }
}

