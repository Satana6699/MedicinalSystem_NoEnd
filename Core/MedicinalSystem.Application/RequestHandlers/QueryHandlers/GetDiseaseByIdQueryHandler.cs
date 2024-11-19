using MediatR;
using AutoMapper;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers;

public class GetDiseaseByIdQueryHandler : IRequestHandler<GetDiseaseByIdQuery, DiseaseDto?>
{
	private readonly IDiseaseRepository _repository;
	private readonly IMapper _mapper;

	public GetDiseaseByIdQueryHandler(IDiseaseRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<DiseaseDto?> Handle(GetDiseaseByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<DiseaseDto>(await _repository.GetById(request.Id, trackChanges: false));
}
