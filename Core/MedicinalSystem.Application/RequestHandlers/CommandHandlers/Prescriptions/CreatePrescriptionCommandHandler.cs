using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands.Prescriptions;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers.Prescriptions;

public class CreatePrescriptionCommandHandler : IRequestHandler<CreatePrescriptionCommand>
{
    private readonly IPrescriptionRepository _repository;
    private readonly IMapper _mapper;

    public CreatePrescriptionCommandHandler(IPrescriptionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
    {
        await _repository.Create(_mapper.Map<Prescription>(request.Prescription));
        await _repository.SaveChanges();
    }
}
