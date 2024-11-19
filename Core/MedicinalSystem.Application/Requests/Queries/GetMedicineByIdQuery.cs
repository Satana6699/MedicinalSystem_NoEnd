using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetMedicineByIdQuery(Guid Id) : IRequest<MedicineDto?>;
