using MediatR;

namespace MedicinalSystem.Application.Requests.Commands.Diseases;

public record DeleteDiseaseCommand(Guid Id) : IRequest<bool>;
