using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetSymptomsQuery : IRequest<PagedResult<SymptomDto>>
{
    public int Page { get; }
    public int PageSize { get; }
    public GetSymptomsQuery()
    {

    }
    public GetSymptomsQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}
