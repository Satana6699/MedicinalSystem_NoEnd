using MediatR;
using MedicinalSystem.Application.Dtos.FamilyMembers;

namespace MedicinalSystem.Application.Requests.Queries.FamilyMembers;

public record GetFamilyMembersAllQuery(string? name) : IRequest<IEnumerable<FamilyMemberDto>>;