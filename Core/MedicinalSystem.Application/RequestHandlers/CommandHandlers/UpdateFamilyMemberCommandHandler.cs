using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers;

public class UpdateFamilyMemberCommandHandler : IRequestHandler<UpdateFamilyMemberCommand, bool>
{
	private readonly IFamilyMemberRepository _repository;
	private readonly IMapper _mapper;

	public UpdateFamilyMemberCommandHandler(IFamilyMemberRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateFamilyMemberCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.FamilyMember.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.FamilyMember, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
