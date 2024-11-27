using MediatR;
using AutoMapper;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers;

public class GetDiseasesAllQueryHandler : IRequestHandler<GetDiseasesAllQuery, IEnumerable<DiseaseDto>>
{
	private readonly IDiseaseRepository _repository;
	private readonly IMapper _mapper;

	public GetDiseasesAllQueryHandler(IDiseaseRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<DiseaseDto>> Handle(GetDiseasesAllQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<DiseaseDto>>(await _repository.Get(trackChanges: false));
}
