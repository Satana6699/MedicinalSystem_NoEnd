using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Commands;

public record UpdateDiseaseCommand(DiseaseForUpdateDto Disease) : IRequest<bool>;
