using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands.Genders;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers.Genders;

public class UpdateGenderCommandHandler : IRequestHandler<UpdateGenderCommand, bool>
{
    private readonly IGenderRepository _repository;
    private readonly IMapper _mapper;

    public UpdateGenderCommandHandler(IGenderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateGenderCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Gender.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

        _mapper.Map(request.Gender, entity);

        _repository.Update(entity);
        await _repository.SaveChanges();

        return true;
    }
}
