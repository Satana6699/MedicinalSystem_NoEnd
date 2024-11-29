//using FluentAssertions;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System.Net;
//using MedicinalSystem.Application.Requests.Queries;
//using MedicinalSystem.Application.Requests.Commands;
//using MedicinalSystem.Web.Controllers;
//using MedicinalSystem.Application.Dtos.DiseaseSymptoms;

//namespace MedicinalSystem.Tests.ControllersTests;

//public class DiseaseSymptomControllerTests
//{
//    private readonly Mock<IMediator> _mediatorMock;
//    private readonly DiseaseSymptomController _controller;

//    public DiseaseSymptomControllerTests()
//    {
//        _mediatorMock = new Mock<IMediator>();
//        _controller = new DiseaseSymptomController(_mediatorMock.Object);
//    }

//    [Fact]
//    public async Task Get_ReturnsListOfDiseaseSymptoms()
//    {
//        // Arrange
//        var diseaseSymptoms = new List<DiseaseSymptomDto> { new(), new() };
//        var pagedResult = new PagedResult<DiseaseSymptomDto>(diseaseSymptoms, diseaseSymptoms.Count, 1, 10);

//        _mediatorMock
//            .Setup(m => m.Send(It.IsAny<GetDiseaseSymptomsQuery>(), CancellationToken.None))
//            .ReturnsAsync(pagedResult); // Çהוסü ןונוהא¸ל PagedResult, א םו List

//        // Act
//        var result = await _controller.Get();

//        // Assert
//        result.Should().NotBeNull();
//        result.Should().BeOfType(typeof(OkObjectResult));

//        var okResult = result as OkObjectResult;
//        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

//        var value = okResult?.Value as PagedResult<DiseaseSymptomDto>;
//        value.Should().NotBeNull();
//        value?.Items.Should().HaveCount(2);
//        value?.Items.Should().BeEquivalentTo(diseaseSymptoms);

//        _mediatorMock.Verify(m => m.Send(It.IsAny<GetDiseaseSymptomsQuery>(), CancellationToken.None), Times.Once);
//    }


//    [Fact]
//    public async Task GetById_ExistingDiseaseSymptomId_ReturnsDiseaseSymptom()
//    {
//        // Arrange
//        var diseaseSymptomId = Guid.NewGuid();
//        var diseaseSymptom = new DiseaseSymptomDto { Id = diseaseSymptomId };

//        _mediatorMock
//            .Setup(m => m.Send(new GetDiseaseSymptomByIdQuery(diseaseSymptomId), CancellationToken.None))
//            .ReturnsAsync(diseaseSymptom);

//        // Act
//        var result = await _controller.GetById(diseaseSymptomId);

//        // Assert
//        result.Should().NotBeNull();
//        result.Should().BeOfType(typeof(OkObjectResult));

//        var okResult = result as OkObjectResult;
//        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
//        (okResult?.Value as DiseaseSymptomDto).Should().BeEquivalentTo(diseaseSymptom);

//        _mediatorMock.Verify(m => m.Send(new GetDiseaseSymptomByIdQuery(diseaseSymptomId), CancellationToken.None), Times.Once);
//    }

//    [Fact]
//    public async Task GetById_NotExistingDiseaseSymptomId_ReturnsNotFoundResult()
//    {
//        // Arrange
//        var diseaseSymptomId = Guid.NewGuid();
//        var diseaseSymptom = new DiseaseSymptomDto { Id = diseaseSymptomId };

//        _mediatorMock
//            .Setup(m => m.Send(new GetDiseaseSymptomByIdQuery(diseaseSymptomId), CancellationToken.None))
//            .ReturnsAsync((DiseaseSymptomDto?)null);

//        // Act
//        var result = await _controller.GetById(diseaseSymptomId);

//        // Assert
//        result.Should().NotBeNull();
//        result.Should().BeOfType(typeof(NotFoundObjectResult));
//        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

//        _mediatorMock.Verify(m => m.Send(new GetDiseaseSymptomByIdQuery(diseaseSymptomId), CancellationToken.None), Times.Once);
//    }

//    [Fact]
//    public async Task Create_DiseaseSymptom_ReturnsDiseaseSymptom()
//    {
//        // Arrange
//        var diseaseSymptom = new DiseaseSymptomForCreationDto();

//        _mediatorMock.Setup(m => m.Send(new CreateDiseaseSymptomCommand(diseaseSymptom), CancellationToken.None));

//        // Act
//        var result = await _controller.Create(diseaseSymptom);

//        // Assert
//        result.Should().NotBeNull();
//        result.Should().BeOfType(typeof(CreatedAtActionResult));

//        var createdResult = result as CreatedAtActionResult;
//        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
//        (createdResult?.Value as DiseaseSymptomForCreationDto).Should().BeEquivalentTo(diseaseSymptom);

//        _mediatorMock.Verify(m => m.Send(new CreateDiseaseSymptomCommand(diseaseSymptom), CancellationToken.None), Times.Once);
//    }

//    [Fact]
//    public async Task Create_NullValue_ReturnsBadRequest()
//    {
//        // Arrange and Act
//        var result = await _controller.Create(null);

//        // Assert
//        result.Should().NotBeNull();
//        result.Should().BeOfType(typeof(BadRequestObjectResult));
//        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

//        _mediatorMock.Verify(m => m.Send(new CreateDiseaseSymptomCommand(It.IsAny<DiseaseSymptomForCreationDto>()), CancellationToken.None), Times.Never);
//    }

//    [Fact]
//    public async Task Update_ExistingDiseaseSymptom_ReturnsNoContentResult()
//    {
//        // Arrange
//        var diseaseSymptomId = Guid.NewGuid();
//        var diseaseSymptom = new DiseaseSymptomForUpdateDto { Id = diseaseSymptomId };

//        _mediatorMock
//            .Setup(m => m.Send(new UpdateDiseaseSymptomCommand(diseaseSymptom), CancellationToken.None))
//            .ReturnsAsync(true);

//        // Act
//        var result = await _controller.Update(diseaseSymptomId, diseaseSymptom);

//        // Assert
//        result.Should().NotBeNull();
//        result.Should().BeOfType(typeof(NoContentResult));
//        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

//        _mediatorMock.Verify(m => m.Send(new UpdateDiseaseSymptomCommand(diseaseSymptom), CancellationToken.None), Times.Once);
//    }

//    [Fact]
//    public async Task Update_NotExistingDiseaseSymptom_ReturnsNotFoundResult()
//    {
//        // Arrange
//        var diseaseSymptomId = Guid.NewGuid();
//        var diseaseSymptom = new DiseaseSymptomForUpdateDto { Id = diseaseSymptomId };

//        _mediatorMock
//            .Setup(m => m.Send(new UpdateDiseaseSymptomCommand(diseaseSymptom), CancellationToken.None))
//            .ReturnsAsync(false);

//        // Act
//        var result = await _controller.Update(diseaseSymptomId, diseaseSymptom);

//        // Assert
//        result.Should().NotBeNull();
//        result.Should().BeOfType(typeof(NotFoundObjectResult));
//        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

//        _mediatorMock.Verify(m => m.Send(new UpdateDiseaseSymptomCommand(diseaseSymptom), CancellationToken.None), Times.Once);
//    }

//    [Fact]
//    public async Task Update_NullValue_ReturnsBadRequest()
//    {
//        // Arrange
//        var diseaseSymptomId = Guid.NewGuid();

//        // Act
//        var result = await _controller.Update(diseaseSymptomId, null);

//        // Assert
//        result.Should().NotBeNull();
//        result.Should().BeOfType(typeof(BadRequestObjectResult));
//        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

//        _mediatorMock.Verify(m => m.Send(new UpdateDiseaseSymptomCommand(It.IsAny<DiseaseSymptomForUpdateDto>()), CancellationToken.None), Times.Never);
//    }

//    [Fact]
//    public async Task Delete_ExistingDiseaseSymptomId_ReturnsNoContentResult()
//    {
//        // Arrange
//        var diseaseSymptomId = Guid.NewGuid();

//        _mediatorMock
//            .Setup(m => m.Send(new DeleteDiseaseSymptomCommand(diseaseSymptomId), CancellationToken.None))
//            .ReturnsAsync(true);

//        // Act
//        var result = await _controller.Delete(diseaseSymptomId);

//        // Assert
//        result.Should().NotBeNull();
//        result.Should().BeOfType(typeof(NoContentResult));
//        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

//        _mediatorMock.Verify(m => m.Send(new DeleteDiseaseSymptomCommand(diseaseSymptomId), CancellationToken.None), Times.Once);
//    }

//    [Fact]
//    public async Task Delete_NotExistingDiseaseSymptomId_ReturnsNotFoundResult()
//    {
//        // Arrange
//        var diseaseSymptomId = Guid.NewGuid();

//        _mediatorMock
//            .Setup(m => m.Send(new DeleteDiseaseSymptomCommand(diseaseSymptomId), CancellationToken.None))
//            .ReturnsAsync(false);

//        // Act
//        var result = await _controller.Delete(diseaseSymptomId);

//        // Assert
//        result.Should().NotBeNull();
//        result.Should().BeOfType(typeof(NotFoundObjectResult));
//        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

//        _mediatorMock.Verify(m => m.Send(new DeleteDiseaseSymptomCommand(diseaseSymptomId), CancellationToken.None), Times.Once);
//    }
//}

