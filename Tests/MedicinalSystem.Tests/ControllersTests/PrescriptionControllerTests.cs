using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Application.Requests.Queries;
using MedicinalSystem.Application.Requests.Commands;
using MedicinalSystem.Web.Controllers;

namespace MedicinalSystem.Tests.ControllersTests;

public class PrescriptionControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly PrescriptionController _controller;

    public PrescriptionControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new PrescriptionController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ReturnsListOfPrescriptions()
    {
        // Arrange
        var prescriptions = new List<PrescriptionDto> { new(), new() };

        _mediatorMock
            .Setup(m => m.Send(new GetPrescriptionsQuery(), CancellationToken.None))
            .ReturnsAsync(prescriptions);

        // Act
        var result = await _controller.Get();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var value = okResult?.Value as List<PrescriptionDto>;
        value.Should().HaveCount(2);
        value.Should().BeEquivalentTo(prescriptions);

        _mediatorMock.Verify(m => m.Send(new GetPrescriptionsQuery(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_ExistingPrescriptionId_ReturnsPrescription()
    {
        // Arrange
        var prescriptionId = Guid.NewGuid();
        var prescription = new PrescriptionDto { Id = prescriptionId };

        _mediatorMock
            .Setup(m => m.Send(new GetPrescriptionByIdQuery(prescriptionId), CancellationToken.None))
            .ReturnsAsync(prescription);

        // Act
        var result = await _controller.GetById(prescriptionId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as PrescriptionDto).Should().BeEquivalentTo(prescription);

        _mediatorMock.Verify(m => m.Send(new GetPrescriptionByIdQuery(prescriptionId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingPrescriptionId_ReturnsNotFoundResult()
    {
        // Arrange
        var prescriptionId = Guid.NewGuid();
        var prescription = new PrescriptionDto { Id = prescriptionId };

        _mediatorMock
            .Setup(m => m.Send(new GetPrescriptionByIdQuery(prescriptionId), CancellationToken.None))
            .ReturnsAsync((PrescriptionDto?)null);

        // Act
        var result = await _controller.GetById(prescriptionId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetPrescriptionByIdQuery(prescriptionId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_Prescription_ReturnsPrescription()
    {
        // Arrange
        var prescription = new PrescriptionForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreatePrescriptionCommand(prescription), CancellationToken.None));

        // Act
        var result = await _controller.Create(prescription);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as PrescriptionForCreationDto).Should().BeEquivalentTo(prescription);

        _mediatorMock.Verify(m => m.Send(new CreatePrescriptionCommand(prescription), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreatePrescriptionCommand(It.IsAny<PrescriptionForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingPrescription_ReturnsNoContentResult()
    {
        // Arrange
        var prescriptionId = Guid.NewGuid();
        var prescription = new PrescriptionForUpdateDto { Id = prescriptionId };

        _mediatorMock
            .Setup(m => m.Send(new UpdatePrescriptionCommand(prescription), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(prescriptionId, prescription);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdatePrescriptionCommand(prescription), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingPrescription_ReturnsNotFoundResult()
    {
        // Arrange
        var prescriptionId = Guid.NewGuid();
        var prescription = new PrescriptionForUpdateDto { Id = prescriptionId };

        _mediatorMock
            .Setup(m => m.Send(new UpdatePrescriptionCommand(prescription), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(prescriptionId, prescription);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdatePrescriptionCommand(prescription), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var prescriptionId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(prescriptionId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdatePrescriptionCommand(It.IsAny<PrescriptionForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingPrescriptionId_ReturnsNoContentResult()
    {
        // Arrange
        var prescriptionId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeletePrescriptionCommand(prescriptionId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(prescriptionId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeletePrescriptionCommand(prescriptionId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingPrescriptionId_ReturnsNotFoundResult()
    {
        // Arrange
        var prescriptionId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeletePrescriptionCommand(prescriptionId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(prescriptionId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeletePrescriptionCommand(prescriptionId), CancellationToken.None), Times.Once);
    }
}

