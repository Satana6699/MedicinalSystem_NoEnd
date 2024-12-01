using MediatR;
using MedicinalSystem.Application.Dtos.Medicines;

namespace MedicinalSystem.Application.Requests.Queries.Medicines;

public record GetMedicineByIdQuery(Guid Id) : IRequest<MedicineDto?>;
