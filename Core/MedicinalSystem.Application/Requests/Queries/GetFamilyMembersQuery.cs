﻿using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetFamilyMembersQuery : IRequest<IEnumerable<FamilyMemberDto>>;
