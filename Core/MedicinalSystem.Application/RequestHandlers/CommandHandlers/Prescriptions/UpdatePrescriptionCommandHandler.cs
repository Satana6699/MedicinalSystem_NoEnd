using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands.Prescriptions;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers.Prescriptions;

public class UpdatePrescriptionCommandHandler : IRequestHandler<UpdatePrescriptionCommand, bool>
{
    private readonly IPrescriptionRepository _repository;
    private readonly IMapper _mapper;

    public UpdatePrescriptionCommandHandler(IPrescriptionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdatePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Prescription.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

        _mapper.Map(request.Prescription, entity);

        _repository.Update(entity);
        await _repository.SaveChanges();

        return true;
    }
}
