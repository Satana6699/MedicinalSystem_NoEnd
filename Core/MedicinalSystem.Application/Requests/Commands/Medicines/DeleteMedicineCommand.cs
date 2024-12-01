using MediatR;

namespace MedicinalSystem.Application.Requests.Commands.Medicines;

public record DeleteMedicineCommand(Guid Id) : IRequest<bool>;
