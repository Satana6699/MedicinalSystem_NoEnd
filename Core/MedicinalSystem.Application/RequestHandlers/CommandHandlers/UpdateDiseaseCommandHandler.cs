using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers;

public class UpdateDiseaseCommandHandler : IRequestHandler<UpdateDiseaseCommand, bool>
{
	private readonly IDiseaseRepository _repository;
	private readonly IMapper _mapper;

	public UpdateDiseaseCommandHandler(IDiseaseRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateDiseaseCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.Disease.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.Disease, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
