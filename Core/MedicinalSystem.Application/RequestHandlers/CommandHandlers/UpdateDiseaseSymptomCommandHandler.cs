using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers;

public class UpdateDiseaseSymptomCommandHandler : IRequestHandler<UpdateDiseaseSymptomCommand, bool>
{
	private readonly IDiseaseSymptomRepository _repository;
	private readonly IMapper _mapper;

	public UpdateDiseaseSymptomCommandHandler(IDiseaseSymptomRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateDiseaseSymptomCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.DiseaseSymptom.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.DiseaseSymptom, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
