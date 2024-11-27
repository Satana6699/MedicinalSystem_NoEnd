﻿using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetMedicinesQuery : IRequest<IEnumerable<MedicineDto>>
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
