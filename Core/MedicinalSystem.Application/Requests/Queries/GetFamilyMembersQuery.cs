﻿using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetFamilyMembersQuery : IRequest<PagedResult<FamilyMemberDto>>
{
    public int Page { get; }
    public int PageSize { get; }
    public string? Name { get; }
    public GetFamilyMembersQuery(int page, int pageSize, string? name)
    {
        Page = page;
        PageSize = pageSize;
        Name = name;
    }
}