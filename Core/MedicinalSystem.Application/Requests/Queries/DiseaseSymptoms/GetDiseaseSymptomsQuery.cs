using MediatR;
using MedicinalSystem.Application.Dtos.DiseaseSymptoms;

namespace MedicinalSystem.Application.Requests.Queries.DiseaseSymptoms;

public record GetDiseaseSymptomsQuery : IRequest<PagedResult<DiseaseSymptomDto>>
{
    public int Page { get; }
    public int PageSize { get; }
    public string? NameDisease { get; }
    public string? NameSymptom { get; }

    public GetDiseaseSymptomsQuery(int page, int pageSize, string nameDisease, string nameSymptom)
    {
        Page = page;
        PageSize = pageSize;
        NameDisease = nameDisease;
        NameSymptom = nameSymptom;
    }
}
