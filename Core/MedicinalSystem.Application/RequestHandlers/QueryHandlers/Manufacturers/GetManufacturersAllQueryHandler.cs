using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Application.Dtos.Manufacturers;
using MedicinalSystem.Application.Requests.Queries.Manufacturers;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.Manufacturers;

public class GetManufacturersAllQueryHandler : IRequestHandler<GetManufacturersAllQuery, IEnumerable<ManufacturerDto>>
{
    private readonly IManufacturerRepository _repository;
    private readonly IMapper _mapper;

    public GetManufacturersAllQueryHandler(IManufacturerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ManufacturerDto>> Handle(GetManufacturersAllQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<IEnumerable<ManufacturerDto>>(await _repository.Get(trackChanges: false));
}
