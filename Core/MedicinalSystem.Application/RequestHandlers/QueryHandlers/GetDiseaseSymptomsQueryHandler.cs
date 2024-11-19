using MediatR;
using AutoMapper;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers;

public class GetDiseaseSymptomsQueryHandler : IRequestHandler<GetDiseaseSymptomsQuery, IEnumerable<DiseaseSymptomDto>>
{
	private readonly IDiseaseSymptomRepository _repository;
	private readonly IMapper _mapper;

	public GetDiseaseSymptomsQueryHandler(IDiseaseSymptomRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<DiseaseSymptomDto>> Handle(GetDiseaseSymptomsQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<DiseaseSymptomDto>>(await _repository.Get(trackChanges: false));
}
