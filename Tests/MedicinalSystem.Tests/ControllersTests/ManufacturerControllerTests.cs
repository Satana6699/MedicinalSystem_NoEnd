using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using MedicinalSystem.Application.Requests.Queries.Manufacturers;
using MedicinalSystem.Application.Requests.Commands.Manufacturers;
using MedicinalSystem.Web.Controllers.SingleRecords;
using MedicinalSystem.Application.Dtos.Manufacturers;

namespace MedicinalSystem.Tests.ControllersTests;

public class ManufacturerControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ManufacturerController _controller;

    public ManufacturerControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new ManufacturerController(_mediatorMock.Object);
    }


    [Fact]
    public async Task GetById_ExistingManufacturerId_ReturnsManufacturer()
    {
        // Arrange
        var manufacturerId = Guid.NewGuid();
        var manufacturer = new ManufacturerDto { Id = manufacturerId };

        _mediatorMock
            .Setup(m => m.Send(new GetManufacturerByIdQuery(manufacturerId), CancellationToken.None))
            .ReturnsAsync(manufacturer);

        // Act
        var result = await _controller.GetById(manufacturerId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as ManufacturerDto).Should().BeEquivalentTo(manufacturer);

        _mediatorMock.Verify(m => m.Send(new GetManufacturerByIdQuery(manufacturerId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingManufacturerId_ReturnsNotFoundResult()
    {
        // Arrange
        var manufacturerId = Guid.NewGuid();
        var manufacturer = new ManufacturerDto { Id = manufacturerId };

        _mediatorMock
            .Setup(m => m.Send(new GetManufacturerByIdQuery(manufacturerId), CancellationToken.None))
            .ReturnsAsync((ManufacturerDto?)null);

        // Act
        var result = await _controller.GetById(manufacturerId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetManufacturerByIdQuery(manufacturerId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_Manufacturer_ReturnsManufacturer()
    {
        // Arrange
        var manufacturer = new ManufacturerForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateManufacturerCommand(manufacturer), CancellationToken.None));

        // Act
        var result = await _controller.Create(manufacturer);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as ManufacturerForCreationDto).Should().BeEquivalentTo(manufacturer);

        _mediatorMock.Verify(m => m.Send(new CreateManufacturerCommand(manufacturer), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateManufacturerCommand(It.IsAny<ManufacturerForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingManufacturer_ReturnsNoContentResult()
    {
        // Arrange
        var manufacturerId = Guid.NewGuid();
        var manufacturer = new ManufacturerForUpdateDto { Id = manufacturerId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateManufacturerCommand(manufacturer), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(manufacturerId, manufacturer);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateManufacturerCommand(manufacturer), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingManufacturer_ReturnsNotFoundResult()
    {
        // Arrange
        var manufacturerId = Guid.NewGuid();
        var manufacturer = new ManufacturerForUpdateDto { Id = manufacturerId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateManufacturerCommand(manufacturer), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(manufacturerId, manufacturer);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateManufacturerCommand(manufacturer), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var manufacturerId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(manufacturerId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateManufacturerCommand(It.IsAny<ManufacturerForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingManufacturerId_ReturnsNoContentResult()
    {
        // Arrange
        var manufacturerId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteManufacturerCommand(manufacturerId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(manufacturerId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteManufacturerCommand(manufacturerId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingManufacturerId_ReturnsNotFoundResult()
    {
        // Arrange
        var manufacturerId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteManufacturerCommand(manufacturerId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(manufacturerId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteManufacturerCommand(manufacturerId), CancellationToken.None), Times.Once);
    }
}

