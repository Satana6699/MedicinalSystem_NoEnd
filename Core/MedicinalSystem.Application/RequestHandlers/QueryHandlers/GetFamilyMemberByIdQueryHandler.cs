using MediatR;
using AutoMapper;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers;

public class GetFamilyMemberByIdQueryHandler : IRequestHandler<GetFamilyMemberByIdQuery, FamilyMemberDto?>
{
	private readonly IFamilyMemberRepository _repository;
	private readonly IMapper _mapper;

	public GetFamilyMemberByIdQueryHandler(IFamilyMemberRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<FamilyMemberDto?> Handle(GetFamilyMemberByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<FamilyMemberDto>(await _repository.GetById(request.Id, trackChanges: false));
}
