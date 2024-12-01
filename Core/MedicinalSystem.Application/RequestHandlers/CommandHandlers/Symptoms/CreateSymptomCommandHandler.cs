using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands.Symptoms;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers.Symptoms;

public class CreateSymptomCommandHandler : IRequestHandler<CreateSymptomCommand>
{
    private readonly ISymptomRepository _repository;
    private readonly IMapper _mapper;

    public CreateSymptomCommandHandler(ISymptomRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(CreateSymptomCommand request, CancellationToken cancellationToken)
    {
        await _repository.Create(_mapper.Map<Symptom>(request.Symptom));
        await _repository.SaveChanges();
    }
}
