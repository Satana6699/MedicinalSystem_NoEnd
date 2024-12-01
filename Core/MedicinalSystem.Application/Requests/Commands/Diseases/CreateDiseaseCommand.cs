using MediatR;
using MedicinalSystem.Application.Dtos.Diseases;

namespace MedicinalSystem.Application.Requests.Commands.Diseases;

public record CreateDiseaseCommand(DiseaseForCreationDto Disease) : IRequest;
