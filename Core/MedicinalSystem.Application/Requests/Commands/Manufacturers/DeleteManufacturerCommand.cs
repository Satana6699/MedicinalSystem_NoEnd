using MediatR;

namespace MedicinalSystem.Application.Requests.Commands.Manufacturers;

public record DeleteManufacturerCommand(Guid Id) : IRequest<bool>;
