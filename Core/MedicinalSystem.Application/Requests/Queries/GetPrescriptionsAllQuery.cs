using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetPrescriptionsAllQuery : IRequest<IEnumerable<PrescriptionDto>>
{
    public string? Name { get; set; }
    public GetPrescriptionsAllQuery(string? name)
    {
        Name = name;
    }
}

