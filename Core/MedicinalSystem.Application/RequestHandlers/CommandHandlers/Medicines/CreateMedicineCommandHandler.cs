using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands.Medicines;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers.Medicines;

public class CreateMedicineCommandHandler : IRequestHandler<CreateMedicineCommand, Medicine>
{
	private readonly IMedicineRepository _repository;
	private readonly IMapper _mapper;

	public CreateMedicineCommandHandler(IMedicineRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<Medicine> Handle(CreateMedicineCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.Create(_mapper.Map<Medicine>(request.Medicine));
		await _repository.SaveChanges();
		return entity;
	}
}
