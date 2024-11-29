using MediatR;
using MedicinalSystem.Application.Dtos.MedicinePrices;

namespace MedicinalSystem.Application.Requests.Queries.MedicinePrices;

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
