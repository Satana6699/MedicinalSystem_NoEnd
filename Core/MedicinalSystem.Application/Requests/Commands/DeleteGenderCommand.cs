using MediatR;

namespace MedicinalSystem.Application.Requests.Commands;

public record DeleteGenderCommand(Guid Id) : IRequest<bool>;
