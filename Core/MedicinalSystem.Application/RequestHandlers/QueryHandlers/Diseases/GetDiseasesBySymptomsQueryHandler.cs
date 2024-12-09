using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Dtos.Diseases;
using MedicinalSystem.Application.Requests.Queries.Diseases;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.Diseases;

public class GetDiseasesBySymptomsQueryHandler : IRequestHandler<GetDiseasesBySymptomsQuery, PagedResult<DiseaseDto>>
{
    private readonly IDiseaseRepository _repository;
    private readonly IMapper _mapper;

    public GetDiseasesBySymptomsQueryHandler(IDiseaseRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedResult<DiseaseDto>> Handle(GetDiseasesBySymptomsQuery request, CancellationToken cancellationToken)
    {
        var totalItems = await _repository.CountAsync(request.SymptomIds);
        var diseases = await _repository.GetPageDiseasesBySymptomsAsync(request.Page, request.PageSize, request.SymptomIds);

        var items = _mapper.Map<IEnumerable<DiseaseDto>>(diseases);
        return new PagedResult<DiseaseDto>(items, totalItems, request.Page, request.PageSize);
    }
}
