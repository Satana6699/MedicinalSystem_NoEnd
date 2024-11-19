using MediatR;

namespace MedicinalSystem.Application.Requests.Commands;

public record DeleteDiseaseCommand(Guid Id) : IRequest<bool>;
