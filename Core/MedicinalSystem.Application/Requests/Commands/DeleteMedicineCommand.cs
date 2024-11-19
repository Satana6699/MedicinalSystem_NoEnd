using MediatR;

namespace MedicinalSystem.Application.Requests.Commands;

public record DeleteMedicineCommand(Guid Id) : IRequest<bool>;
