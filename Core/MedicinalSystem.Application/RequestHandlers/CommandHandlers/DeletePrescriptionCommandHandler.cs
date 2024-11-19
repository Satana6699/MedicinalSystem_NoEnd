using MediatR;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers;

public class DeletePrescriptionCommandHandler(IPrescriptionRepository repository) : IRequestHandler<DeletePrescriptionCommand, bool>
{
	private readonly IPrescriptionRepository _repository = repository;

	public async Task<bool> Handle(DeletePrescriptionCommand request, CancellationToken cancellationToken)
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
