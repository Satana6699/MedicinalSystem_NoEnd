using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Commands;

public record UpdateMedicineCommand(MedicineForUpdateDto Medicine) : IRequest<bool>;
