using MediatR;
using MedicinalSystem.Application.Dtos.Manufacturers;

namespace MedicinalSystem.Application.Requests.Commands.Manufacturers;

public record UpdateManufacturerCommand(ManufacturerForUpdateDto Manufacturer) : IRequest<bool>;
