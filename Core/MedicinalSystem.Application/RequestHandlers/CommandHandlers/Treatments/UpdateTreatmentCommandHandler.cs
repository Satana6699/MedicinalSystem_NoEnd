using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands.Treatments;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers.Treatments;

public class UpdateTreatmentCommandHandler : IRequestHandler<UpdateTreatmentCommand, bool>
{
    private readonly ITreatmentRepository _repository;
    private readonly IMapper _mapper;

    public UpdateTreatmentCommandHandler(ITreatmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateTreatmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Treatment.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

        _mapper.Map(request.Treatment, entity);

        _repository.Update(entity);
        await _repository.SaveChanges();

        return true;
    }
}
