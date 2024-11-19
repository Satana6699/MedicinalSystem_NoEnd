using MediatR;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers;

public class DeleteFamilyMemberCommandHandler(IFamilyMemberRepository repository) : IRequestHandler<DeleteFamilyMemberCommand, bool>
{
	private readonly IFamilyMemberRepository _repository = repository;

	public async Task<bool> Handle(DeleteFamilyMemberCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.Id, trackChanges: false);

        if (entity is null)
        {
            return false;
        }

        _repository.Delete(entity);
        await _repository.SaveChanges();

        return true;
	}
}
