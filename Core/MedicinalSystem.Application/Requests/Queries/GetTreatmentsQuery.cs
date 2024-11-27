using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetTreatmentsQuery : IRequest<IEnumerable<TreatmentDto>>;