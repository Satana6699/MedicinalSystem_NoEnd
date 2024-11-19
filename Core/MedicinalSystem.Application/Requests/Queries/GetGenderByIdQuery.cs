using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetGenderByIdQuery(Guid Id) : IRequest<GenderDto?>;
