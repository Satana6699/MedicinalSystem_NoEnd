using MediatR;

namespace MedicinalSystem.Application.Requests.Commands.Genders;

public record DeleteGenderCommand(Guid Id) : IRequest<bool>;
