using MediatR;
using MedicinalSystem.Application.Dtos.FamilyMembers;

namespace MedicinalSystem.Application.Requests.Queries.FamilyMembers;

public record GetFamilyMemberByIdQuery(Guid Id) : IRequest<FamilyMemberDto?>;
