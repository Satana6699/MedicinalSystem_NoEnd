using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Commands;

public record CreateManufacturerCommand(ManufacturerForCreationDto Manufacturer) : IRequest;
