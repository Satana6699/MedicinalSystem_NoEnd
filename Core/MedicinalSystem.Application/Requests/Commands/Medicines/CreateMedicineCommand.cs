using MediatR;
using MedicinalSystem.Application.Dtos.Medicines;

namespace MedicinalSystem.Application.Requests.Commands.Medicines;

public record CreateMedicineCommand(MedicineForCreationDto Medicine) : IRequest;
