using MediatR;

namespace MedicinalSystem.Application.Requests.Commands;

public record DeleteFamilyMemberCommand(Guid Id) : IRequest<bool>;
