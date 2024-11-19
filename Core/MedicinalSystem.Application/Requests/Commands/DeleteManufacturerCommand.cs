using MediatR;

namespace MedicinalSystem.Application.Requests.Commands;

public record DeleteManufacturerCommand(Guid Id) : IRequest<bool>;
