﻿using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetFamilyMemberByIdQuery(Guid Id) : IRequest<FamilyMemberDto?>;
