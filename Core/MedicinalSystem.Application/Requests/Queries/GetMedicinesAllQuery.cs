using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetMedicinesAllQuery : IRequest<IEnumerable<MedicineDto>>
{
    public string? Name { get; set; }
    public GetMedicinesAllQuery(string? name)
    {
        Name = name;
    }
}
