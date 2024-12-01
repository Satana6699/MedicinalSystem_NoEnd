using MediatR;
using MedicinalSystem.Application.Dtos.Medicines;

namespace MedicinalSystem.Application.Requests.Queries.Medicines;

public record GetMedicinesAllQuery : IRequest<IEnumerable<MedicineDto>>
{
    public string? Name { get; set; }
    public GetMedicinesAllQuery(string? name)
    {
        Name = name;
    }
}
