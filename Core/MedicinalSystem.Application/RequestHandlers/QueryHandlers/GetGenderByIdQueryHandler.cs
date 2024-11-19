using MediatR;
using AutoMapper;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers;

public class GetGenderByIdQueryHandler : IRequestHandler<GetGenderByIdQuery, GenderDto?>
{
	private readonly IGenderRepository _repository;
	private readonly IMapper _mapper;

	public GetGenderByIdQueryHandler(IGenderRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<GenderDto?> Handle(GetGenderByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<GenderDto>(await _repository.GetById(request.Id, trackChanges: false));
}
