using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands.Diseases;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers.Diseases;

public class CreateDiseaseCommandHandler : IRequestHandler<CreateDiseaseCommand>
{
    private readonly IDiseaseRepository _repository;
    private readonly IMapper _mapper;

    public CreateDiseaseCommandHandler(IDiseaseRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(CreateDiseaseCommand request, CancellationToken cancellationToken)
    {
        await _repository.Create(_mapper.Map<Disease>(request.Disease));
        await _repository.SaveChanges();
    }
}
