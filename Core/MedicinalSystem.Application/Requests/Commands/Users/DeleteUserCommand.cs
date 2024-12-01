using MediatR;

namespace MedicinalSystem.Application.Requests.Commands.Users;

public record DeleteUserCommand(Guid Id) : IRequest<bool>;
