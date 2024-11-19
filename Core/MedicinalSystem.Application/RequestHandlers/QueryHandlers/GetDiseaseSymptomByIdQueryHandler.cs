using MediatR;
using AutoMapper;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers;

public class GetDiseaseSymptomByIdQueryHandler : IRequestHandler<GetDiseaseSymptomByIdQuery, DiseaseSymptomDto?>
{
	private readonly IDiseaseSymptomRepository _repository;
	private readonly IMapper _mapper;

	public GetDiseaseSymptomByIdQueryHandler(IDiseaseSymptomRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<DiseaseSymptomDto?> Handle(GetDiseaseSymptomByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<DiseaseSymptomDto>(await _repository.GetById(request.Id, trackChanges: false));
}
