using MediatR;

namespace MedicinalSystem.Application.Requests.Commands;

public record DeletePrescriptionCommand(Guid Id) : IRequest<bool>;
