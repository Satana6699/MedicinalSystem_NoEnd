using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries.Treatments;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Application.Dtos.Treatments;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.Treatments;

public class GetTreatmentsQueryHandler : IRequestHandler<GetTreatmentsQuery, PagedResult<TreatmentDto>>
{
    private readonly ITreatmentRepository _repository;
    private readonly IMapper _mapper;

    public GetTreatmentsQueryHandler(ITreatmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedResult<TreatmentDto>> Handle(GetTreatmentsQuery request, CancellationToken cancellationToken)
    {
        var totalItems = await _repository.CountAsync(request.Name);
        var treatments = await _repository.GetPageAsync(request.Page, request.PageSize, request.Name);

        var items = _mapper.Map<IEnumerable<TreatmentDto>>(treatments);
        return new PagedResult<TreatmentDto>(items, totalItems, request.Page, request.PageSize);
    }
}
