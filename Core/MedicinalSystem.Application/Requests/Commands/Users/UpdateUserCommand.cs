using MediatR;
using MedicinalSystem.Application.Dtos.Users;

namespace MedicinalSystem.Application.Requests.Commands.Users;

public record UpdateUserCommand(UserForUpdateDto User) : IRequest<bool>;
