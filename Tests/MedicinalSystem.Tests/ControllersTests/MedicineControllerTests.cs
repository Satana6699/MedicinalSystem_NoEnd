using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using MedicinalSystem.Application.Requests.Queries.Medicines;
using MedicinalSystem.Application.Requests.Commands.Medicines;
using MedicinalSystem.Web.Controllers.MultipleRecords;
using MedicinalSystem.Application.Dtos.Medicines;

namespace MedicinalSystem.Tests.ControllersTests;

public class MedicineControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly MedicineController _controller;

    public MedicineControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new MedicineController(_mediatorMock.Object);
    }


    [Fact]
    public async Task GetById_ExistingMedicineId_ReturnsMedicine()
    {
        // Arrange
        var medicineId = Guid.NewGuid();
        var medicine = new MedicineDto { Id = medicineId };

        _mediatorMock
            .Setup(m => m.Send(new GetMedicineByIdQuery(medicineId), CancellationToken.None))
            .ReturnsAsync(medicine);

        // Act
        var result = await _controller.GetById(medicineId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as MedicineDto).Should().BeEquivalentTo(medicine);

        _mediatorMock.Verify(m => m.Send(new GetMedicineByIdQuery(medicineId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingMedicineId_ReturnsNotFoundResult()
    {
        // Arrange
        var medicineId = Guid.NewGuid();
        var medicine = new MedicineDto { Id = medicineId };

        _mediatorMock
            .Setup(m => m.Send(new GetMedicineByIdQuery(medicineId), CancellationToken.None))
            .ReturnsAsync((MedicineDto?)null);

        // Act
        var result = await _controller.GetById(medicineId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetMedicineByIdQuery(medicineId), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateMedicineCommand(It.IsAny<MedicineForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingMedicine_ReturnsNoContentResult()
    {
        // Arrange
        var medicineId = Guid.NewGuid();
        var medicine = new MedicineForUpdateDto { Id = medicineId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateMedicineCommand(medicine), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(medicineId, medicine);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateMedicineCommand(medicine), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingMedicine_ReturnsNotFoundResult()
    {
        // Arrange
        var medicineId = Guid.NewGuid();
        var medicine = new MedicineForUpdateDto { Id = medicineId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateMedicineCommand(medicine), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(medicineId, medicine);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateMedicineCommand(medicine), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var medicineId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(medicineId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateMedicineCommand(It.IsAny<MedicineForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingMedicineId_ReturnsNoContentResult()
    {
        // Arrange
        var medicineId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteMedicineCommand(medicineId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(medicineId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteMedicineCommand(medicineId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingMedicineId_ReturnsNotFoundResult()
    {
        // Arrange
        var medicineId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteMedicineCommand(medicineId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(medicineId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteMedicineCommand(medicineId), CancellationToken.None), Times.Once);
    }
}

