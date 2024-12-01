using MediatR;
using MedicinalSystem.Application.Dtos.Genders;

namespace MedicinalSystem.Application.Requests.Queries.Genders;

public record GetGenderByIdQuery(Guid Id) : IRequest<GenderDto?>;
