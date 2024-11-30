using MediatR;
using MedicinalSystem.Application.Dtos.Users;

namespace MedicinalSystem.Application.Requests.Queries.Users;

public record GetUserByIdQuery(Guid Id) : IRequest<UserDto?>;