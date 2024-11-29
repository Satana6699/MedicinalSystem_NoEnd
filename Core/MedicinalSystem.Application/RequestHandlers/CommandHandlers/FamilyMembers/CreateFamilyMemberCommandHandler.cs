using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands.FamilyMembers;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers.FamilyMembers;

public class CreateFamilyMemberCommandHandler : IRequestHandler<CreateFamilyMemberCommand>
{
    private readonly IFamilyMemberRepository _repository;
    private readonly IMapper _mapper;

    public CreateFamilyMemberCommandHandler(IFamilyMemberRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(CreateFamilyMemberCommand request, CancellationToken cancellationToken)
    {
        await _repository.Create(_mapper.Map<FamilyMember>(request.FamilyMember));
        await _repository.SaveChanges();
    }
}
