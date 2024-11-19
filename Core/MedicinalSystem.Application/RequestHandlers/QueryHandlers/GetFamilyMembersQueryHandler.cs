using MediatR;
using AutoMapper;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers;

public class GetFamilyMembersQueryHandler : IRequestHandler<GetFamilyMembersQuery, IEnumerable<FamilyMemberDto>>
{
	private readonly IFamilyMemberRepository _repository;
	private readonly IMapper _mapper;

	public GetFamilyMembersQueryHandler(IFamilyMemberRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<FamilyMemberDto>> Handle(GetFamilyMembersQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<FamilyMemberDto>>(await _repository.Get(trackChanges: false));
}
