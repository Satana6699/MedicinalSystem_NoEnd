using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers;

public class UpdateManufacturerCommandHandler : IRequestHandler<UpdateManufacturerCommand, bool>
{
	private readonly IManufacturerRepository _repository;
	private readonly IMapper _mapper;

	public UpdateManufacturerCommandHandler(IManufacturerRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateManufacturerCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.Manufacturer.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.Manufacturer, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
