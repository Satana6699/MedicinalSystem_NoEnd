using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries.Manufacturers;
using MedicinalSystem.Application.Dtos.Manufacturers;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.Manufacturers;

public class GetManufacturersQueryHandler : IRequestHandler<GetManufacturersQuery, PagedResult<ManufacturerDto>>
{
    private readonly IManufacturerRepository _repository;
    private readonly IMapper _mapper;

    public GetManufacturersQueryHandler(IManufacturerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedResult<ManufacturerDto>> Handle(GetManufacturersQuery request, CancellationToken cancellationToken)
    {
        var totalItems = await _repository.CountAsync(request.Name);
        var manufacturers = await _repository.GetPageAsync(request.Page, request.PageSize, request.Name);

        var items = _mapper.Map<IEnumerable<ManufacturerDto>>(manufacturers);
        return new PagedResult<ManufacturerDto>(items, totalItems, request.Page, request.PageSize);
    }
}
