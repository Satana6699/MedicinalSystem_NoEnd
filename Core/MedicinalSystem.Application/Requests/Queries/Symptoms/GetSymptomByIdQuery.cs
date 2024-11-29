using MediatR;
using MedicinalSystem.Application.Dtos.Symptoms;

namespace MedicinalSystem.Application.Requests.Queries.Symptoms;

public record GetSymptomByIdQuery(Guid Id) : IRequest<SymptomDto?>;
