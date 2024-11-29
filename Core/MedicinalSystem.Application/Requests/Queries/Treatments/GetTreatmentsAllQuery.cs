using MediatR;
using MedicinalSystem.Application.Dtos.Treatments;

namespace MedicinalSystem.Application.Requests.Queries.Treatments;

public record GetTreatmentsAllQuery : IRequest<IEnumerable<TreatmentDto>>;