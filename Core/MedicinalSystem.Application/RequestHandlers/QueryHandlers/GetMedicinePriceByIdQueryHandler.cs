using MediatR;
using AutoMapper;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers;

public class GetMedicinePriceByIdQueryHandler : IRequestHandler<GetMedicinePriceByIdQuery, MedicinePriceDto?>
{
	private readonly IMedicinePriceRepository _repository;
	private readonly IMapper _mapper;

	public GetMedicinePriceByIdQueryHandler(IMedicinePriceRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<MedicinePriceDto?> Handle(GetMedicinePriceByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<MedicinePriceDto>(await _repository.GetById(request.Id, trackChanges: false));
}
