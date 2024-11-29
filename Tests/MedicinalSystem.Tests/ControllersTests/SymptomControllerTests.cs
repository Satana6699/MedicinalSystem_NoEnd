//using FluentAssertions;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System.Net;
//using MedicinalSystem.Application.Requests.Queries;
//using MedicinalSystem.Application.Requests.Commands;
//using MedicinalSystem.Web.Controllers;
//using MedicinalSystem.Application.Dtos.Symptoms;

//namespace MedicinalSystem.Tests.ControllersTests;

//public class SymptomControllerTests
//{
//    private readonly Mock<IMediator> _mediatorMock;
//    private readonly SymptomController _controller;

//    public SymptomControllerTests()
//    {
//        _mediatorMock = new Mock<IMediator>();
//        _controller = new SymptomController(_mediatorMock.Object);
//    }

//    //[Fact]
//    //public async Task Get_ReturnsListOfSymptoms()
//    //{
//    //    // Arrange
//    //    var symptoms = new List<SymptomDto> { new(), new() };
//    //    var pagedResult = new PagedResult<SymptomDto>(symptoms, symptoms.Count, 1, 10);
        
//    //    //_mediatorMock
//    //    //    .Setup(m => m.Send(new GetSymptomsQuery(), CancellationToken.None))
//    //    //    .ReturnsAsync(symptoms);

//    //    _mediatorMock
//    //        .Setup(m => m.Send(It.IsAny<GetSymptomsQuery>(), It.IsAny<CancellationToken>()))
//    //        .ReturnsAsync(pagedResult);
//    //    // Act
//    //    var result = await _controller.Get();

//    //    // Assert
//    //    result.Should().NotBeNull();
//    //    result.Should().BeOfType(typeof(OkObjectResult));

//    //    var okResult = result as OkObjectResult;
//    //    okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

//    //    var value = okResult?.Value as List<SymptomDto>;
//    //    value.Should().HaveCount(2);
//    //    value.Should().BeEquivalentTo(symptoms);

//    //    _mediatorMock.Verify(m => m.Send(new GetSymptomsQuery(), CancellationToken.None), Times.Once);
//    //}

//    [Fact]
//    public async Task GetById_ExistingSymptomId_ReturnsSymptom()
//    {
//        // Arrange
//        var symptomId = Guid.NewGuid();
//        var symptom = new SymptomDto { Id = symptomId };

//        _mediatorMock
//            .Setup(m => m.Send(new GetSymptomByIdQuery(symptomId), CancellationToken.None))
//            .ReturnsAsync(symptom);

//        // Act
//        var result = await _controller.GetById(symptomId);

//        // Assert
//        result.Should().NotBeNull();
//        result.Should().BeOfType(typeof(OkObjectResult));

//        var okResult = result as OkObjectResult;
//        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
//        (okResult?.Value as SymptomDto).Should().BeEquivalentTo(symptom);

//        _mediatorMock.Verify(m => m.Send(new GetSymptomByIdQuery(symptomId), CancellationToken.None), Times.Once);
//    }

//    [Fact]
//    public async Task GetById_NotExistingSymptomId_ReturnsNotFoundResult()
//    {
//        // Arrange
//        var symptomId = Guid.NewGuid();
//        var symptom = new SymptomDto { Id = symptomId };

//        _mediatorMock
//            .Setup(m => m.Send(new GetSymptomByIdQuery(symptomId), CancellationToken.None))
//            .ReturnsAsync((SymptomDto?)null);

//        // Act
//        var result = await _controller.GetById(symptomId);

//        // Assert
//        result.Should().NotBeNull();
//        result.Should().BeOfType(typeof(NotFoundObjectResult));
//        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

//        _mediatorMock.Verify(m => m.Send(new GetSymptomByIdQuery(symptomId), CancellationToken.None), Times.Once);
//    }

//    [Fact]
//    public async Task Create_Symptom_ReturnsSymptom()
//    {
//        // Arrange
//        var symptom = new SymptomForCreationDto();

//        _mediatorMock.Setup(m => m.Send(new CreateSymptomCommand(symptom), CancellationToken.None));

//        // Act
//        var result = await _controller.Create(symptom);

//        // Assert
//        result.Should().NotBeNull();
//        result.Should().BeOfType(typeof(CreatedAtActionResult));

//        var createdResult = result as CreatedAtActionResult;
//        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
//        (createdResult?.Value as SymptomForCreationDto).Should().BeEquivalentTo(symptom);

//        _mediatorMock.Verify(m => m.Send(new CreateSymptomCommand(symptom), CancellationToken.None), Times.Once);
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

//        _mediatorMock.Verify(m => m.Send(new CreateSymptomCommand(It.IsAny<SymptomForCreationDto>()), CancellationToken.None), Times.Never);
//    }

//    [Fact]
//    public async Task Update_ExistingSymptom_ReturnsNoContentResult()
//    {
//        // Arrange
//        var symptomId = Guid.NewGuid();
//        var symptom = new SymptomForUpdateDto { Id = symptomId };

//        _mediatorMock
//            .Setup(m => m.Send(new UpdateSymptomCommand(symptom), CancellationToken.None))
//            .ReturnsAsync(true);

//        // Act
//        var result = await _controller.Update(symptomId, symptom);

//        // Assert
//        result.Should().NotBeNull();
//        result.Should().BeOfType(typeof(NoContentResult));
//        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

//        _mediatorMock.Verify(m => m.Send(new UpdateSymptomCommand(symptom), CancellationToken.None), Times.Once);
//    }

//    [Fact]
//    public async Task Update_NotExistingSymptom_ReturnsNotFoundResult()
//    {
//        // Arrange
//        var symptomId = Guid.NewGuid();
//        var symptom = new SymptomForUpdateDto { Id = symptomId };

//        _mediatorMock
//            .Setup(m => m.Send(new UpdateSymptomCommand(symptom), CancellationToken.None))
//            .ReturnsAsync(false);

//        // Act
//        var result = await _controller.Update(symptomId, symptom);

//        // Assert
//        result.Should().NotBeNull();
//        result.Should().BeOfType(typeof(NotFoundObjectResult));
//        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

//        _mediatorMock.Verify(m => m.Send(new UpdateSymptomCommand(symptom), CancellationToken.None), Times.Once);
//    }

//    [Fact]
//    public async Task Update_NullValue_ReturnsBadRequest()
//    {
//        // Arrange
//        var symptomId = Guid.NewGuid();

//        // Act
//        var result = await _controller.Update(symptomId, null);

//        // Assert
//        result.Should().NotBeNull();
//        result.Should().BeOfType(typeof(BadRequestObjectResult));
//        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

//        _mediatorMock.Verify(m => m.Send(new UpdateSymptomCommand(It.IsAny<SymptomForUpdateDto>()), CancellationToken.None), Times.Never);
//    }

//    [Fact]
//    public async Task Delete_ExistingSymptomId_ReturnsNoContentResult()
//    {
//        // Arrange
//        var symptomId = Guid.NewGuid();

//        _mediatorMock
//            .Setup(m => m.Send(new DeleteSymptomCommand(symptomId), CancellationToken.None))
//            .ReturnsAsync(true);

//        // Act
//        var result = await _controller.Delete(symptomId);

//        // Assert
//        result.Should().NotBeNull();
//        result.Should().BeOfType(typeof(NoContentResult));
//        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

//        _mediatorMock.Verify(m => m.Send(new DeleteSymptomCommand(symptomId), CancellationToken.None), Times.Once);
//    }

//    [Fact]
//    public async Task Delete_NotExistingSymptomId_ReturnsNotFoundResult()
//    {
//        // Arrange
//        var symptomId = Guid.NewGuid();

//        _mediatorMock
//            .Setup(m => m.Send(new DeleteSymptomCommand(symptomId), CancellationToken.None))
//            .ReturnsAsync(false);

//        // Act
//        var result = await _controller.Delete(symptomId);

//        // Assert
//        result.Should().NotBeNull();
//        result.Should().BeOfType(typeof(NotFoundObjectResult));
//        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

//        _mediatorMock.Verify(m => m.Send(new DeleteSymptomCommand(symptomId), CancellationToken.None), Times.Once);
//    }
//}

