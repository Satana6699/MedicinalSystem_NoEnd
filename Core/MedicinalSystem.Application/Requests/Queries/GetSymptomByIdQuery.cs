using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetSymptomByIdQuery(Guid Id) : IRequest<SymptomDto?>;
