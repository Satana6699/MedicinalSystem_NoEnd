using MediatR;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers;

public class DeleteMedicinePriceCommandHandler(IMedicinePriceRepository repository) : IRequestHandler<DeleteMedicinePriceCommand, bool>
{
	private readonly IMedicinePriceRepository _repository = repository;

	public async Task<bool> Handle(DeleteMedicinePriceCommand request, CancellationToken cancellationToken)
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
