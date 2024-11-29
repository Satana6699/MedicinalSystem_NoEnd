using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries.Manufacturers;
using MedicinalSystem.Application.Dtos.Manufacturers;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.Manufacturers;

public class GetManufacturerByIdQueryHandler : IRequestHandler<GetManufacturerByIdQuery, ManufacturerDto?>
{
    private readonly IManufacturerRepository _repository;
    private readonly IMapper _mapper;

    public GetManufacturerByIdQueryHandler(IManufacturerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ManufacturerDto?> Handle(GetManufacturerByIdQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<ManufacturerDto>(await _repository.GetById(request.Id, trackChanges: false));
}
