using MediatR;
using AutoMapper;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers;

public class GetSymptomByIdQueryHandler : IRequestHandler<GetSymptomByIdQuery, SymptomDto?>
{
	private readonly ISymptomRepository _repository;
	private readonly IMapper _mapper;

	public GetSymptomByIdQueryHandler(ISymptomRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<SymptomDto?> Handle(GetSymptomByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<SymptomDto>(await _repository.GetById(request.Id, trackChanges: false));
}
