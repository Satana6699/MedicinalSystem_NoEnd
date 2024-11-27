using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetManufacturersQuery : IRequest<PagedResult<ManufacturerDto>>
{
    public int Page { get; }
    public int PageSize { get; }
    public string? Name { get; }
    public GetManufacturersQuery(int page, int pageSize, string? name)
    {
        Page = page;
        PageSize = pageSize;
        Name = name;
    }
}
