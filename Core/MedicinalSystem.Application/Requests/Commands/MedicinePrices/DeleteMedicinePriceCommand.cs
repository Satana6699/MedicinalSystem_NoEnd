using MediatR;

namespace MedicinalSystem.Application.Requests.Commands.MedicinePrices;

public record DeleteMedicinePriceCommand(Guid Id) : IRequest<bool>;
