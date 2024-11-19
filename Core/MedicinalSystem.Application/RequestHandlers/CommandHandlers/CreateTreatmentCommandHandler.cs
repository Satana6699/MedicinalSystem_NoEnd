using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers;

public class CreateTreatmentCommandHandler : IRequestHandler<CreateTreatmentCommand>
{
	private readonly ITreatmentRepository _repository;
	private readonly IMapper _mapper;

	public CreateTreatmentCommandHandler(ITreatmentRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateTreatmentCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<Treatment>(request.Treatment));
		await _repository.SaveChanges();
	}
}
