using MediatR;
using MedicinalSystem.Application.Dtos.Manufacturers;

namespace MedicinalSystem.Application.Requests.Queries.Manufacturers;

public record GetManufacturerByIdQuery(Guid Id) : IRequest<ManufacturerDto?>;
