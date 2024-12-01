using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Dtos.MedicinePrices;
using MedicinalSystem.Application.Requests.Queries.MedicinePrices;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.MedicinePrices;

public class GetMedicinePricesQueryHandler : IRequestHandler<GetMedicinePricesQuery, PagedResult<MedicinePriceDto>>
{
    private readonly IMedicinePriceRepository _repository;
    private readonly IMapper _mapper;

    public GetMedicinePricesQueryHandler(IMedicinePriceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedResult<MedicinePriceDto>> Handle(GetMedicinePricesQuery request, CancellationToken cancellationToken)
    {
        var totalItems = await _repository.CountAsync(request.Name);
        var medicinePrices = await _repository.GetPageAsync(request.Page, request.PageSize, request.Name);

        var items = _mapper.Map<IEnumerable<MedicinePriceDto>>(medicinePrices);
        return new PagedResult<MedicinePriceDto>(items, totalItems, request.Page, request.PageSize);
    }
}
