using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Dtos.Diseases;
using MedicinalSystem.Application.Requests.Queries.Diseases;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.Diseases;

public class GetDiseasesQueryHandler : IRequestHandler<GetDiseasesQuery, PagedResult<DiseaseDto>>
{
    private readonly IDiseaseRepository _repository;
    private readonly IMapper _mapper;

    public GetDiseasesQueryHandler(IDiseaseRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedResult<DiseaseDto>> Handle(GetDiseasesQuery request, CancellationToken cancellationToken)
    {
        var totalItems = await _repository.CountAsync(request.Name);
        var diseaeses = await _repository.GetPageAsync(request.Page, request.PageSize, request.Name);

        var items = _mapper.Map<IEnumerable<DiseaseDto>>(diseaeses);
        return new PagedResult<DiseaseDto>(items, totalItems, request.Page, request.PageSize);
    }
}
