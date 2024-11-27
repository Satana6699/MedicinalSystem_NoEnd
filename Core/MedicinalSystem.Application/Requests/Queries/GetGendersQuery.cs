using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetGendersQuery : IRequest<PagedResult<GenderDto>>
{
    public int Page { get; }
    public int PageSize { get; }
    public string? Name { get; }
    public GetGendersQuery(int page, int pageSize, string? name)
    {
        Page = page;
        PageSize = pageSize;
        Name = name;
    }
}

