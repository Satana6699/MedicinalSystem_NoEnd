using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Dtos.FamilyMembers;
using MedicinalSystem.Application.Requests.Queries.FamilyMembers;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.FamilyMembers;

public class GetFamilyMembersAllQueryHandler : IRequestHandler<GetFamilyMembersAllQuery, IEnumerable<FamilyMemberDto>>
{
    private readonly IFamilyMemberRepository _repository;
    private readonly IMapper _mapper;

    public GetFamilyMembersAllQueryHandler(IFamilyMemberRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FamilyMemberDto>> Handle(GetFamilyMembersAllQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<IEnumerable<FamilyMemberDto>>(await _repository.Get(trackChanges: false));
}
