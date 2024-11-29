using MediatR;
using MedicinalSystem.Application.Dtos.Symptoms;

namespace MedicinalSystem.Application.Requests.Queries.Symptoms;
public class GetSymptomsAllQuery : IRequest<IEnumerable<SymptomDto>>
{
    string? Name { get; set; }
    public GetSymptomsAllQuery(string? name)
    {
        Name = name;
    }
}
