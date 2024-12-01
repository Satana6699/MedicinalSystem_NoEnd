using MediatR;
using MedicinalSystem.Application.Dtos.Prescriptions;

namespace MedicinalSystem.Application.Requests.Queries.Prescriptions;

public record GetPrescriptionsAllQuery : IRequest<IEnumerable<PrescriptionDto>>
{
    public string? Name { get; set; }
    public GetPrescriptionsAllQuery(string? name)
    {
        Name = name;
    }
}

