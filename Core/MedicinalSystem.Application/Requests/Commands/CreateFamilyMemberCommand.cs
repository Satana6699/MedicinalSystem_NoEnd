using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Commands;

public record CreateFamilyMemberCommand(FamilyMemberForCreationDto FamilyMember) : IRequest;
