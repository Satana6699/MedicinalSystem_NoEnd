using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Dtos.Prescriptions;
using MedicinalSystem.Application.Requests.Queries.Prescriptions;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.Prescriptions;

public class GetPrescriptionsQueryHandler : IRequestHandler<GetPrescriptionsQuery, PagedResult<PrescriptionDto>>
{
    private readonly IPrescriptionRepository _repository;
    private readonly IMapper _mapper;

    public GetPrescriptionsQueryHandler(IPrescriptionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedResult<PrescriptionDto>> Handle(GetPrescriptionsQuery request, CancellationToken cancellationToken)
    {
        var totalItems = await _repository.CountAsync(request.Name);
        var prescriptions = await _repository.GetPageAsync(request.Page, request.PageSize, request.Name);

        var items = _mapper.Map<IEnumerable<PrescriptionDto>>(prescriptions);
        return new PagedResult<PrescriptionDto>(items, totalItems, request.Page, request.PageSize);
    }
}
