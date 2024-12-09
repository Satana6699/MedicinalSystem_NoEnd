using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using MedicinalSystem.Application.Requests.Queries.Genders;
using MedicinalSystem.Application.Requests.Commands.Genders;
using MedicinalSystem.Web.Controllers.SingleRecords;
using MedicinalSystem.Application.Dtos.Genders;

namespace MedicinalSystem.Tests.ControllersTests;

public class GenderControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly GenderController _controller;

    public GenderControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new GenderController(_mediatorMock.Object);
    }


    [Fact]
    public async Task GetById_ExistingGenderId_ReturnsGender()
    {
        // Arrange
        var genderId = Guid.NewGuid();
        var gender = new GenderDto { Id = genderId };

        _mediatorMock
            .Setup(m => m.Send(new GetGenderByIdQuery(genderId), CancellationToken.None))
            .ReturnsAsync(gender);

        // Act
        var result = await _controller.GetById(genderId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as GenderDto).Should().BeEquivalentTo(gender);

        _mediatorMock.Verify(m => m.Send(new GetGenderByIdQuery(genderId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingGenderId_ReturnsNotFoundResult()
    {
        // Arrange
        var genderId = Guid.NewGuid();
        var gender = new GenderDto { Id = genderId };

        _mediatorMock
            .Setup(m => m.Send(new GetGenderByIdQuery(genderId), CancellationToken.None))
            .ReturnsAsync((GenderDto?)null);

        // Act
        var result = await _controller.GetById(genderId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetGenderByIdQuery(genderId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_Gender_ReturnsGender()
    {
        // Arrange
        var gender = new GenderForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateGenderCommand(gender), CancellationToken.None));

        // Act
        var result = await _controller.Create(gender);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as GenderForCreationDto).Should().BeEquivalentTo(gender);

        _mediatorMock.Verify(m => m.Send(new CreateGenderCommand(gender), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateGenderCommand(It.IsAny<GenderForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingGender_ReturnsNoContentResult()
    {
        // Arrange
        var genderId = Guid.NewGuid();
        var gender = new GenderForUpdateDto { Id = genderId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateGenderCommand(gender), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(genderId, gender);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateGenderCommand(gender), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingGender_ReturnsNotFoundResult()
    {
        // Arrange
        var genderId = Guid.NewGuid();
        var gender = new GenderForUpdateDto { Id = genderId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateGenderCommand(gender), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(genderId, gender);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateGenderCommand(gender), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var genderId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(genderId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateGenderCommand(It.IsAny<GenderForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingGenderId_ReturnsNoContentResult()
    {
        // Arrange
        var genderId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteGenderCommand(genderId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(genderId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteGenderCommand(genderId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingGenderId_ReturnsNotFoundResult()
    {
        // Arrange
        var genderId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteGenderCommand(genderId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(genderId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteGenderCommand(genderId), CancellationToken.None), Times.Once);
    }
}

