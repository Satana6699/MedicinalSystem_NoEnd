using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetTreatmentsAllQuery : IRequest<IEnumerable<TreatmentDto>>
{
    public int Page { get; }
    public int PageSize { get; }
    public string? Name { get; }
    public GetTreatmentsAllQuery(int page, int pageSize, string? name)
    {
        Page = page;
        PageSize = pageSize;
        Name = name;
    }
}
