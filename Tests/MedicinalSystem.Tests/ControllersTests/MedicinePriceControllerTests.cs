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

public class MedicinePriceControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly MedicinePriceController _controller;

    public MedicinePriceControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new MedicinePriceController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ReturnsListOfMedicinePrices()
    {
        // Arrange
        var medicinePrices = new List<MedicinePriceDto> { new(), new() };

        _mediatorMock
            .Setup(m => m.Send(new GetMedicinePricesQuery(), CancellationToken.None))
            .ReturnsAsync(medicinePrices);

        // Act
        var result = await _controller.Get();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var value = okResult?.Value as List<MedicinePriceDto>;
        value.Should().HaveCount(2);
        value.Should().BeEquivalentTo(medicinePrices);

        _mediatorMock.Verify(m => m.Send(new GetMedicinePricesQuery(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_ExistingMedicinePriceId_ReturnsMedicinePrice()
    {
        // Arrange
        var medicinePriceId = Guid.NewGuid();
        var medicinePrice = new MedicinePriceDto { Id = medicinePriceId };

        _mediatorMock
            .Setup(m => m.Send(new GetMedicinePriceByIdQuery(medicinePriceId), CancellationToken.None))
            .ReturnsAsync(medicinePrice);

        // Act
        var result = await _controller.GetById(medicinePriceId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as MedicinePriceDto).Should().BeEquivalentTo(medicinePrice);

        _mediatorMock.Verify(m => m.Send(new GetMedicinePriceByIdQuery(medicinePriceId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingMedicinePriceId_ReturnsNotFoundResult()
    {
        // Arrange
        var medicinePriceId = Guid.NewGuid();
        var medicinePrice = new MedicinePriceDto { Id = medicinePriceId };

        _mediatorMock
            .Setup(m => m.Send(new GetMedicinePriceByIdQuery(medicinePriceId), CancellationToken.None))
            .ReturnsAsync((MedicinePriceDto?)null);

        // Act
        var result = await _controller.GetById(medicinePriceId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetMedicinePriceByIdQuery(medicinePriceId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_MedicinePrice_ReturnsMedicinePrice()
    {
        // Arrange
        var medicinePrice = new MedicinePriceForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateMedicinePriceCommand(medicinePrice), CancellationToken.None));

        // Act
        var result = await _controller.Create(medicinePrice);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as MedicinePriceForCreationDto).Should().BeEquivalentTo(medicinePrice);

        _mediatorMock.Verify(m => m.Send(new CreateMedicinePriceCommand(medicinePrice), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateMedicinePriceCommand(It.IsAny<MedicinePriceForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingMedicinePrice_ReturnsNoContentResult()
    {
        // Arrange
        var medicinePriceId = Guid.NewGuid();
        var medicinePrice = new MedicinePriceForUpdateDto { Id = medicinePriceId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateMedicinePriceCommand(medicinePrice), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(medicinePriceId, medicinePrice);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateMedicinePriceCommand(medicinePrice), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingMedicinePrice_ReturnsNotFoundResult()
    {
        // Arrange
        var medicinePriceId = Guid.NewGuid();
        var medicinePrice = new MedicinePriceForUpdateDto { Id = medicinePriceId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateMedicinePriceCommand(medicinePrice), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(medicinePriceId, medicinePrice);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateMedicinePriceCommand(medicinePrice), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var medicinePriceId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(medicinePriceId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateMedicinePriceCommand(It.IsAny<MedicinePriceForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingMedicinePriceId_ReturnsNoContentResult()
    {
        // Arrange
        var medicinePriceId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteMedicinePriceCommand(medicinePriceId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(medicinePriceId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteMedicinePriceCommand(medicinePriceId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingMedicinePriceId_ReturnsNotFoundResult()
    {
        // Arrange
        var medicinePriceId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteMedicinePriceCommand(medicinePriceId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(medicinePriceId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteMedicinePriceCommand(medicinePriceId), CancellationToken.None), Times.Once);
    }
}

