using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetTreatmentsQuery : IRequest<PagedResult<TreatmentDto>>

{
    public int Page { get; }
    public int PageSize { get; }
    public string? Name { get; }
    public GetTreatmentsQuery(int page, int pageSize, string? name)
    {
        Page = page;
        PageSize = pageSize;
        Name = name;
    }
}
