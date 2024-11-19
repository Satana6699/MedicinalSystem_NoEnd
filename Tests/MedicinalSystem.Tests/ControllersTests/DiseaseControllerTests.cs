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

public class DiseaseControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly DiseaseController _controller;

    public DiseaseControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new DiseaseController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ReturnsListOfDiseases()
    {
        // Arrange
        var diseases = new List<DiseaseDto> { new(), new() };

        _mediatorMock
            .Setup(m => m.Send(new GetDiseasesQuery(), CancellationToken.None))
            .ReturnsAsync(diseases);

        // Act
        var result = await _controller.Get();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var value = okResult?.Value as List<DiseaseDto>;
        value.Should().HaveCount(2);
        value.Should().BeEquivalentTo(diseases);

        _mediatorMock.Verify(m => m.Send(new GetDiseasesQuery(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_ExistingDiseaseId_ReturnsDisease()
    {
        // Arrange
        var diseaseId = Guid.NewGuid();
        var disease = new DiseaseDto { Id = diseaseId };

        _mediatorMock
            .Setup(m => m.Send(new GetDiseaseByIdQuery(diseaseId), CancellationToken.None))
            .ReturnsAsync(disease);

        // Act
        var result = await _controller.GetById(diseaseId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as DiseaseDto).Should().BeEquivalentTo(disease);

        _mediatorMock.Verify(m => m.Send(new GetDiseaseByIdQuery(diseaseId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingDiseaseId_ReturnsNotFoundResult()
    {
        // Arrange
        var diseaseId = Guid.NewGuid();
        var disease = new DiseaseDto { Id = diseaseId };

        _mediatorMock
            .Setup(m => m.Send(new GetDiseaseByIdQuery(diseaseId), CancellationToken.None))
            .ReturnsAsync((DiseaseDto?)null);

        // Act
        var result = await _controller.GetById(diseaseId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetDiseaseByIdQuery(diseaseId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_Disease_ReturnsDisease()
    {
        // Arrange
        var disease = new DiseaseForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateDiseaseCommand(disease), CancellationToken.None));

        // Act
        var result = await _controller.Create(disease);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as DiseaseForCreationDto).Should().BeEquivalentTo(disease);

        _mediatorMock.Verify(m => m.Send(new CreateDiseaseCommand(disease), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateDiseaseCommand(It.IsAny<DiseaseForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingDisease_ReturnsNoContentResult()
    {
        // Arrange
        var diseaseId = Guid.NewGuid();
        var disease = new DiseaseForUpdateDto { Id = diseaseId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateDiseaseCommand(disease), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(diseaseId, disease);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateDiseaseCommand(disease), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingDisease_ReturnsNotFoundResult()
    {
        // Arrange
        var diseaseId = Guid.NewGuid();
        var disease = new DiseaseForUpdateDto { Id = diseaseId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateDiseaseCommand(disease), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(diseaseId, disease);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateDiseaseCommand(disease), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var diseaseId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(diseaseId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateDiseaseCommand(It.IsAny<DiseaseForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingDiseaseId_ReturnsNoContentResult()
    {
        // Arrange
        var diseaseId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteDiseaseCommand(diseaseId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(diseaseId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteDiseaseCommand(diseaseId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingDiseaseId_ReturnsNotFoundResult()
    {
        // Arrange
        var diseaseId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteDiseaseCommand(diseaseId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(diseaseId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteDiseaseCommand(diseaseId), CancellationToken.None), Times.Once);
    }
}

