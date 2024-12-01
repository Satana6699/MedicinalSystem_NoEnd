using MediatR;

namespace MedicinalSystem.Application.Requests.Commands.Prescriptions;

public record DeletePrescriptionCommand(Guid Id) : IRequest<bool>;
