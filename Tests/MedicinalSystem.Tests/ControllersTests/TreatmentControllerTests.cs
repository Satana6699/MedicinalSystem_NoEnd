using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using MedicinalSystem.Application.Requests.Queries.Treatments;
using MedicinalSystem.Application.Requests.Commands.Treatments;
using MedicinalSystem.Web.Controllers.MultipleRecords;
using MedicinalSystem.Application.Dtos.Treatments;

namespace MedicinalSystem.Tests.ControllersTests;

public class TreatmentControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly TreatmentController _controller;

    public TreatmentControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new TreatmentController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetById_ExistingTreatmentId_ReturnsTreatment()
    {
        // Arrange
        var treatmentId = Guid.NewGuid();
        var treatment = new TreatmentDto { Id = treatmentId };

        _mediatorMock
            .Setup(m => m.Send(new GetTreatmentByIdQuery(treatmentId), CancellationToken.None))
            .ReturnsAsync(treatment);

        // Act
        var result = await _controller.GetById(treatmentId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as TreatmentDto).Should().BeEquivalentTo(treatment);

        _mediatorMock.Verify(m => m.Send(new GetTreatmentByIdQuery(treatmentId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingTreatmentId_ReturnsNotFoundResult()
    {
        // Arrange
        var treatmentId = Guid.NewGuid();
        var treatment = new TreatmentDto { Id = treatmentId };

        _mediatorMock
            .Setup(m => m.Send(new GetTreatmentByIdQuery(treatmentId), CancellationToken.None))
            .ReturnsAsync((TreatmentDto?)null);

        // Act
        var result = await _controller.GetById(treatmentId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetTreatmentByIdQuery(treatmentId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_Treatment_ReturnsTreatment()
    {
        // Arrange
        var treatment = new TreatmentForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateTreatmentCommand(treatment), CancellationToken.None));

        // Act
        var result = await _controller.Create(treatment);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as TreatmentForCreationDto).Should().BeEquivalentTo(treatment);

        _mediatorMock.Verify(m => m.Send(new CreateTreatmentCommand(treatment), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateTreatmentCommand(It.IsAny<TreatmentForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingTreatment_ReturnsNoContentResult()
    {
        // Arrange
        var treatmentId = Guid.NewGuid();
        var treatment = new TreatmentForUpdateDto { Id = treatmentId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateTreatmentCommand(treatment), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(treatmentId, treatment);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateTreatmentCommand(treatment), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingTreatment_ReturnsNotFoundResult()
    {
        // Arrange
        var treatmentId = Guid.NewGuid();
        var treatment = new TreatmentForUpdateDto { Id = treatmentId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateTreatmentCommand(treatment), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(treatmentId, treatment);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateTreatmentCommand(treatment), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var treatmentId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(treatmentId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateTreatmentCommand(It.IsAny<TreatmentForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingTreatmentId_ReturnsNoContentResult()
    {
        // Arrange
        var treatmentId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteTreatmentCommand(treatmentId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(treatmentId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteTreatmentCommand(treatmentId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingTreatmentId_ReturnsNotFoundResult()
    {
        // Arrange
        var treatmentId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteTreatmentCommand(treatmentId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(treatmentId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteTreatmentCommand(treatmentId), CancellationToken.None), Times.Once);
    }
}

