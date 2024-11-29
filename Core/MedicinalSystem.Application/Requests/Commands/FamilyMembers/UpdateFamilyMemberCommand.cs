using MediatR;
using MedicinalSystem.Application.Dtos.FamilyMembers;

namespace MedicinalSystem.Application.Requests.Commands.FamilyMembers;

public record UpdateFamilyMemberCommand(FamilyMemberForUpdateDto FamilyMember) : IRequest<bool>;
