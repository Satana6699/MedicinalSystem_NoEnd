using MediatR;
using AutoMapper;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers;

public class GetManufacturersQueryHandler : IRequestHandler<GetManufacturersQuery, IEnumerable<ManufacturerDto>>
{
	private readonly IManufacturerRepository _repository;
	private readonly IMapper _mapper;

	public GetManufacturersQueryHandler(IManufacturerRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<ManufacturerDto>> Handle(GetManufacturersQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<ManufacturerDto>>(await _repository.Get(trackChanges: false));
}
