using MediatR;

namespace MedicinalSystem.Application.Requests.Commands.FamilyMembers;

public record DeleteFamilyMemberCommand(Guid Id) : IRequest<bool>;
