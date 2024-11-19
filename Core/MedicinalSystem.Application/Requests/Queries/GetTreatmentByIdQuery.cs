using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetTreatmentByIdQuery(Guid Id) : IRequest<TreatmentDto?>;
