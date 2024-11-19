using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetManufacturerByIdQuery(Guid Id) : IRequest<ManufacturerDto?>;
