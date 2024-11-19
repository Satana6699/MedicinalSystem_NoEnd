using MediatR;

namespace MedicinalSystem.Application.Requests.Commands;

public record DeleteMedicinePriceCommand(Guid Id) : IRequest<bool>;
