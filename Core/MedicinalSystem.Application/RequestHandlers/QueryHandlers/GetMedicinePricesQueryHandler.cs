using MediatR;
using AutoMapper;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers;

public class GetMedicinePricesQueryHandler : IRequestHandler<GetMedicinePricesQuery, IEnumerable<MedicinePriceDto>>
{
	private readonly IMedicinePriceRepository _repository;
	private readonly IMapper _mapper;

	public GetMedicinePricesQueryHandler(IMedicinePriceRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<MedicinePriceDto>> Handle(GetMedicinePricesQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<MedicinePriceDto>>(await _repository.Get(trackChanges: false));
}
