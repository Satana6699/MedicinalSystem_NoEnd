using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetSymptomsQuery : IRequest<PagedResult<SymptomDto>>
{
    public int Page { get; }
    public int PageSize { get; }
    public string? Name { get; }
    public GetSymptomsQuery(int page, int pageSize, string? name)
    {
        Page = page;
        PageSize = pageSize;
        Name = name;
    }
}
