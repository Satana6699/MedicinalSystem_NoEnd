using MediatR;
using MedicinalSystem.Application.Dtos.Medicines;
using MedicinalSystem.Domain.Entities;

namespace MedicinalSystem.Application.Requests.Commands.Medicines;

public record CreateMedicineCommand(MedicineForCreationDto Medicine) : IRequest<Medicine>;
