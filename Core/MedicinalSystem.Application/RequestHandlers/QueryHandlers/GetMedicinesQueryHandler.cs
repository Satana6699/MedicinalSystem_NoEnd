using MediatR;
using AutoMapper;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers;

public class GetMedicinesQueryHandler : IRequestHandler<GetMedicinesAllQuery, IEnumerable<MedicineDto>>
{
	private readonly IMedicineRepository _repository;
	private readonly IMapper _mapper;

	public GetMedicinesQueryHandler(IMedicineRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<MedicineDto>> Handle(GetMedicinesAllQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<MedicineDto>>(await _repository.Get(trackChanges: false));
}
