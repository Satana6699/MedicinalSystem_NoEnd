using MediatR;

namespace MedicinalSystem.Application.Requests.Commands.Treatments;

public record DeleteTreatmentCommand(Guid Id) : IRequest<bool>;
