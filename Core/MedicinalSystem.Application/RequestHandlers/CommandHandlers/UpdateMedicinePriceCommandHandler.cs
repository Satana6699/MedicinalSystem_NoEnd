using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers;

public class UpdateMedicinePriceCommandHandler : IRequestHandler<UpdateMedicinePriceCommand, bool>
{
	private readonly IMedicinePriceRepository _repository;
	private readonly IMapper _mapper;

	public UpdateMedicinePriceCommandHandler(IMedicinePriceRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateMedicinePriceCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.MedicinePrice.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.MedicinePrice, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
