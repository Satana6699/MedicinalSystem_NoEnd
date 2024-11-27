using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetDiseasesAllQuery : IRequest<IEnumerable<DiseaseDto>>
{
    public string? Name { get; set; }
    public GetDiseasesAllQuery(string? name)
    {
        Name = name;
    }
}
