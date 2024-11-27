using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;
public class GetSymptomsAllQuery : IRequest<IEnumerable<SymptomDto>>
{
    string? Name { get; set; }
    public GetSymptomsAllQuery(string? name)
    {
        Name = name;
    }
}
