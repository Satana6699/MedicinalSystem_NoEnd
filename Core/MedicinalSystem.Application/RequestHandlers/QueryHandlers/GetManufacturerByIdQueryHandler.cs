using MediatR;
using AutoMapper;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers;

public class GetManufacturerByIdQueryHandler : IRequestHandler<GetManufacturerByIdQuery, ManufacturerDto?>
{
	private readonly IManufacturerRepository _repository;
	private readonly IMapper _mapper;

	public GetManufacturerByIdQueryHandler(IManufacturerRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<ManufacturerDto?> Handle(GetManufacturerByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<ManufacturerDto>(await _repository.GetById(request.Id, trackChanges: false));
}
