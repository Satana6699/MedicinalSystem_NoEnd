using MediatR;
using MedicinalSystem.Application.Dtos.FamilyMembers;

namespace MedicinalSystem.Application.Requests.Commands.FamilyMembers;

public record CreateFamilyMemberCommand(FamilyMemberForCreationDto FamilyMember) : IRequest;
