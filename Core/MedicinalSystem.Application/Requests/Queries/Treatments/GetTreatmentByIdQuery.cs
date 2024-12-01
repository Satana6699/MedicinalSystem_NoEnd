using MediatR;
using MedicinalSystem.Application.Dtos.Treatments;

namespace MedicinalSystem.Application.Requests.Queries.Treatments;

public record GetTreatmentByIdQuery(Guid Id) : IRequest<TreatmentDto?>;