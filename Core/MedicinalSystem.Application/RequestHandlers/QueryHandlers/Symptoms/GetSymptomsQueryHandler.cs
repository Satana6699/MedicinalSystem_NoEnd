using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Application.Dtos.Symptoms;
using MedicinalSystem.Application.Requests.Queries.Symptoms;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.Symptoms;

public class GetSymptomsQueryHandler : IRequestHandler<GetSymptomsQuery, PagedResult<SymptomDto>>
{
    private readonly ISymptomRepository _repository;
    private readonly IMapper _mapper;

    public GetSymptomsQueryHandler(ISymptomRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedResult<SymptomDto>> Handle(GetSymptomsQuery request, CancellationToken cancellationToken)
    {
        var totalItems = await _repository.CountAsync(request.Name);
        var symptoms = await _repository.GetPageAsync(request.Page, request.PageSize, request.Name);

        var items = _mapper.Map<IEnumerable<SymptomDto>>(symptoms);
        return new PagedResult<SymptomDto>(items, totalItems, request.Page, request.PageSize);
    }
}
