using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Dtos.FamilyMembers;
using MedicinalSystem.Application.Requests.Queries.FamilyMembers;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.FamilyMembers;

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
