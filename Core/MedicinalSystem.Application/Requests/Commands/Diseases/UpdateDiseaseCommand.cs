using MediatR;
using MedicinalSystem.Application.Dtos.Diseases;

namespace MedicinalSystem.Application.Requests.Commands.Diseases;

public record UpdateDiseaseCommand(DiseaseForUpdateDto Disease) : IRequest<bool>;
