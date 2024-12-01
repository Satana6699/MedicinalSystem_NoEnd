using MediatR;
using MedicinalSystem.Application.Dtos.Medicines;

namespace MedicinalSystem.Application.Requests.Commands.Medicines;

public record UpdateMedicineCommand(MedicineForUpdateDto Medicine) : IRequest<bool>;
