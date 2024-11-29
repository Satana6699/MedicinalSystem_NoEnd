using MediatR;
using MedicinalSystem.Application.Dtos.Diseases;

namespace MedicinalSystem.Application.Requests.Queries.Diseases;

public record GetDiseasesAllQuery : IRequest<IEnumerable<DiseaseDto>>
{
    public string? Name { get; set; }
    public GetDiseasesAllQuery(string? name)
    {
        Name = name;
    }
}
