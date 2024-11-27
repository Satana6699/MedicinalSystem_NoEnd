using MediatR;
using AutoMapper;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers;

public class GetMedicinesQueryHandler : IRequestHandler<GetMedicinesQuery, PagedResult<MedicineDto>>
{
	private readonly IMedicineRepository _repository;
	private readonly IMapper _mapper;

	public GetMedicinesQueryHandler(IMedicineRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<PagedResult<MedicineDto>> Handle(GetMedicinesQuery request, CancellationToken cancellationToken)
    {
        var totalItems = await _repository.CountAsync(request.Name);
        var medicines = await _repository.GetPageAsync(request.Page, request.PageSize, request.Name);

        var items = _mapper.Map<IEnumerable<MedicineDto>>(medicines);
        return new PagedResult<MedicineDto>(items, totalItems, request.Page, request.PageSize);
    }
}
