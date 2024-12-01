using MediatR;
using MedicinalSystem.Application.Dtos.Medicines;

namespace MedicinalSystem.Application.Requests.Queries.Medicines;

public record GetMedicinesQuery : IRequest<PagedResult<MedicineDto>>
{
    public int Page { get; }
    public int PageSize { get; }
    public string? Name { get; }

    public GetMedicinesQuery(int page, int pageSize, string? name)
    {
        Page = page;
        PageSize = pageSize;
        Name = name;
    }
}
