using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Commands;

public record UpdateFamilyMemberCommand(FamilyMemberForUpdateDto FamilyMember) : IRequest<bool>;
