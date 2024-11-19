using MediatR;

namespace MedicinalSystem.Application.Requests.Commands;

public record DeleteTreatmentCommand(Guid Id) : IRequest<bool>;
