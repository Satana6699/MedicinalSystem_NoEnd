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

public class MedicineControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly MedicineController _controller;

    public MedicineControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new MedicineController(_mediatorMock.Object);
    }

    //[Fact]
    //public async Task Get_ReturnsListOfMedicines()
    //{
    //    // Arrange
    //    var medicines = new List<MedicineDto> { new(), new() };

    //    _mediatorMock
    //        .Setup(m => m.Send(new GetMedicinesQuery(), CancellationToken.None))
    //        .ReturnsAsync(medicines);

    //    // Act
    //    var result = await _controller.Get();

    //    // Assert
    //    result.Should().NotBeNull();
    //    result.Should().BeOfType(typeof(OkObjectResult));

    //    var okResult = result as OkObjectResult;
    //    okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

    //    var value = okResult?.Value as List<MedicineDto>;
    //    value.Should().HaveCount(2);
    //    value.Should().BeEquivalentTo(medicines);

    //    _mediatorMock.Verify(m => m.Send(new GetMedicinesQuery(), CancellationToken.None), Times.Once);
    //}

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
    public async Task Create_Medicine_ReturnsMedicine()
    {
        // Arrange
        var medicine = new MedicineForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateMedicineCommand(medicine), CancellationToken.None));

        // Act
        var result = await _controller.Create(medicine);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as MedicineForCreationDto).Should().BeEquivalentTo(medicine);

        _mediatorMock.Verify(m => m.Send(new CreateMedicineCommand(medicine), CancellationToken.None), Times.Once);
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

