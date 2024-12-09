using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Dtos.Medicines;
using MedicinalSystem.Application.Requests.Queries.Medicines;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.Medicines;

public class GetMedicinesAllQueryHandler : IRequestHandler<GetMedicinesAllQuery, IEnumerable<MedicineDto>>
{
    private readonly IMedicineRepository _repository;
    private readonly IMapper _mapper;

    public GetMedicinesAllQueryHandler(IMedicineRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MedicineDto>> Handle(GetMedicinesAllQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<IEnumerable<MedicineDto>>(await _repository.Get(trackChanges: false));
}
