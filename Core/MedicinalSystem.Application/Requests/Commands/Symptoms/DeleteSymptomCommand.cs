using MediatR;

namespace MedicinalSystem.Application.Requests.Commands.Symptoms;

public record DeleteSymptomCommand(Guid Id) : IRequest<bool>;
