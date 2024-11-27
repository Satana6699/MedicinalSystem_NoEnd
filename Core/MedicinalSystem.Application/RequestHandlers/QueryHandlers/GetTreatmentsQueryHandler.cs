using MediatR;
using AutoMapper;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers;

public class GetTreatmentsQueryHandler : IRequestHandler<GetTreatmentsAllQuery, IEnumerable<TreatmentDto>>
{
	private readonly ITreatmentRepository _repository;
	private readonly IMapper _mapper;

	public GetTreatmentsQueryHandler(ITreatmentRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<TreatmentDto>> Handle(GetTreatmentsAllQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<TreatmentDto>>(await _repository.Get(trackChanges: false));
}
