using MediatR;
using AutoMapper;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers;

public class GetGendersQueryHandler : IRequestHandler<GetGendersQuery, IEnumerable<GenderDto>>
{
	private readonly IGenderRepository _repository;
	private readonly IMapper _mapper;

	public GetGendersQueryHandler(IGenderRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<GenderDto>> Handle(GetGendersQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<GenderDto>>(await _repository.Get(trackChanges: false));
}
