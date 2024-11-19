using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers;

public class UpdateMedicineCommandHandler : IRequestHandler<UpdateMedicineCommand, bool>
{
	private readonly IMedicineRepository _repository;
	private readonly IMapper _mapper;

	public UpdateMedicineCommandHandler(IMedicineRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateMedicineCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.Medicine.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.Medicine, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
