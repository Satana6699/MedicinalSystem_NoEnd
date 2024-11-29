using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Dtos.FamilyMembers;
using MedicinalSystem.Application.Requests.Queries.FamilyMembers;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.FamilyMembers;

public class GetFamilyMembersQueryHandler : IRequestHandler<GetFamilyMembersQuery, PagedResult<FamilyMemberDto>>
{
    private readonly IFamilyMemberRepository _repository;
    private readonly IMapper _mapper;

    public GetFamilyMembersQueryHandler(IFamilyMemberRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedResult<FamilyMemberDto>> Handle(GetFamilyMembersQuery request, CancellationToken cancellationToken)
    {
        var totalItems = await _repository.CountAsync(request.Name);
        var familyMembers = await _repository.GetPageAsync(request.Page, request.PageSize, request.Name);

        var items = _mapper.Map<IEnumerable<FamilyMemberDto>>(familyMembers);
        return new PagedResult<FamilyMemberDto>(items, totalItems, request.Page, request.PageSize);
    }
}
