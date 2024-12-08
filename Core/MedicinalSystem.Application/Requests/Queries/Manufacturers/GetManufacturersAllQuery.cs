using MediatR;
using MedicinalSystem.Application.Dtos.Manufacturers;
using MedicinalSystem.Domain.Entities;

namespace MedicinalSystem.Application.Requests.Queries.Manufacturers;

public record GetManufacturersAllQuery : IRequest<IEnumerable<ManufacturerDto>>
{
    public string? Name { get; set; }
    public GetManufacturersAllQuery(string? name)
    {
        Name = name;
    }
}