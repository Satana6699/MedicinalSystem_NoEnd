﻿using MediatR;
using MedicinalSystem.Application.Dtos.Prescriptions;

namespace MedicinalSystem.Application.Requests.Queries.Prescriptions;

public record GetPrescriptionsQuery : IRequest<PagedResult<PrescriptionDto>>
{
    public int Page { get; }
    public int PageSize { get; }
    public string? Name { get; }
    public GetPrescriptionsQuery(int page, int pageSize, string? name)
    {
        Page = page;
        PageSize = pageSize;
        Name = name;
    }
}
