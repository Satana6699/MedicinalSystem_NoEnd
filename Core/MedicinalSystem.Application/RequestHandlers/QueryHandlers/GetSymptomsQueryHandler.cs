using MediatR;
using AutoMapper;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers;

public class GetSymptomsQueryHandler : IRequestHandler<GetSymptomsQuery, IEnumerable<SymptomDto>>
{
	private readonly ISymptomRepository _repository;
	private readonly IMapper _mapper;

	public GetSymptomsQueryHandler(ISymptomRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<SymptomDto>> Handle(GetSymptomsQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<SymptomDto>>(await _repository.Get(trackChanges: false));
}
