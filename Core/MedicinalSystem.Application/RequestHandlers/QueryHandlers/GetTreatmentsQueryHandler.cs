using MediatR;
using AutoMapper;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries;
using MedicinalSystem.Domain.Entities;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers;

public class GetTreatmentsQueryHandler : IRequestHandler<GetTreatmentsQuery, PagedResult<TreatmentDto>>
{
	private readonly ITreatmentRepository _repository;
	private readonly IMapper _mapper;

	public GetTreatmentsQueryHandler(ITreatmentRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<PagedResult<TreatmentDto>> Handle(GetTreatmentsQuery request, CancellationToken cancellationToken)
	{
		var totalItems = await _repository.CountAsync(request.Name);
		var treatments = await _repository.GetPageAsync(request.Page, request.PageSize, request.Name);

		var items = _mapper.Map<IEnumerable<TreatmentDto>>(treatments);
		return new PagedResult<TreatmentDto>(items, totalItems, request.Page, request.PageSize);
	}
}
