using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Dtos.DiseaseSymptoms;
using MedicinalSystem.Application.Requests.Queries.DiseaseSymptoms;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.DiseaseSymptoms;

public class GetDiseaseSymptomsQueryHandler : IRequestHandler<GetDiseaseSymptomsQuery, PagedResult<DiseaseSymptomDto>>
{
    private readonly IDiseaseSymptomRepository _repository;
    private readonly IMapper _mapper;

    public GetDiseaseSymptomsQueryHandler(IDiseaseSymptomRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedResult<DiseaseSymptomDto>> Handle(GetDiseaseSymptomsQuery request, CancellationToken cancellationToken)
    {
        var totalItems = await _repository.CountAsync(request.NameDisease, request.NameSymptom);
        var diseaseSymptoms = await _repository.GetPageAsync(request.Page, request.PageSize, request.NameDisease, request.NameSymptom);

        var items = _mapper.Map<IEnumerable<DiseaseSymptomDto>>(diseaseSymptoms);
        return new PagedResult<DiseaseSymptomDto>(items, totalItems, request.Page, request.PageSize);
    }
}
