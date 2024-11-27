using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetTreatmentsAllQuery : IRequest<IEnumerable<TreatmentDto>>;