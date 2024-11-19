using MediatR;
using AutoMapper;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers;

public class GetTreatmentByIdQueryHandler : IRequestHandler<GetTreatmentByIdQuery, TreatmentDto?>
{
	private readonly ITreatmentRepository _repository;
	private readonly IMapper _mapper;

	public GetTreatmentByIdQueryHandler(ITreatmentRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<TreatmentDto?> Handle(GetTreatmentByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<TreatmentDto>(await _repository.GetById(request.Id, trackChanges: false));
}
