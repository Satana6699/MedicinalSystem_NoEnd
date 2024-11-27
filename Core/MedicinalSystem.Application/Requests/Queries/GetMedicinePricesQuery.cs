using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetMedicinePricesQuery : IRequest<PagedResult<MedicinePriceDto>>
{
    public int Page { get; }
    public int PageSize { get; }
    public string? Name { get; }
    public GetMedicinePricesQuery(int page, int pageSize, string? name)
    {
        Page = page;
        PageSize = pageSize;
        Name = name;
    }
}
