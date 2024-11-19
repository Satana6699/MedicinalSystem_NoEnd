using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers;

public class CreateMedicinePriceCommandHandler : IRequestHandler<CreateMedicinePriceCommand>
{
	private readonly IMedicinePriceRepository _repository;
	private readonly IMapper _mapper;

	public CreateMedicinePriceCommandHandler(IMedicinePriceRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateMedicinePriceCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<MedicinePrice>(request.MedicinePrice));
		await _repository.SaveChanges();
	}
}
