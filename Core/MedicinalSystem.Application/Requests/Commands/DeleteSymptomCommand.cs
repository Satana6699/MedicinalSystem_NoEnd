using MediatR;

namespace MedicinalSystem.Application.Requests.Commands;

public record DeleteSymptomCommand(Guid Id) : IRequest<bool>;
