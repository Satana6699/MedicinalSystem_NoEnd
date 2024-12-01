using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Dtos.Diseases;
using MedicinalSystem.Application.Requests.Queries.Diseases;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.Diseases;

public class GetDiseasesAllQueryHandler : IRequestHandler<GetDiseasesAllQuery, IEnumerable<DiseaseDto>>
{
    private readonly IDiseaseRepository _repository;
    private readonly IMapper _mapper;

    public GetDiseasesAllQueryHandler(IDiseaseRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DiseaseDto>> Handle(GetDiseasesAllQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<IEnumerable<DiseaseDto>>(await _repository.Get(trackChanges: false));
}
