using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Commands;

public record UpdateManufacturerCommand(ManufacturerForUpdateDto Manufacturer) : IRequest<bool>;
