using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands.Symptoms;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers.Symptoms;

public class UpdateSymptomCommandHandler : IRequestHandler<UpdateSymptomCommand, bool>
{
    private readonly ISymptomRepository _repository;
    private readonly IMapper _mapper;

    public UpdateSymptomCommandHandler(ISymptomRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateSymptomCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Symptom.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

        _mapper.Map(request.Symptom, entity);

        _repository.Update(entity);
        await _repository.SaveChanges();

        return true;
    }
}
