using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands.DiseaseSymptoms;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers.DiseaseSymptoms;

public class CreateDiseaseSymptomCommandHandler : IRequestHandler<CreateDiseaseSymptomCommand>
{
    private readonly IDiseaseSymptomRepository _repository;
    private readonly IMapper _mapper;

    public CreateDiseaseSymptomCommandHandler(IDiseaseSymptomRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(CreateDiseaseSymptomCommand request, CancellationToken cancellationToken)
    {
        await _repository.Create(_mapper.Map<DiseaseSymptom>(request.DiseaseSymptom));
        await _repository.SaveChanges();
    }
}
