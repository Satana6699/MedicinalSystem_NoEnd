using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries.Medicines;
using MedicinalSystem.Application.Dtos.Medicines;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.Medicines;

public class GetMedicineByIdQueryHandler : IRequestHandler<GetMedicineByIdQuery, MedicineDto?>
{
    private readonly IMedicineRepository _repository;
    private readonly IMapper _mapper;

    public GetMedicineByIdQueryHandler(IMedicineRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<MedicineDto?> Handle(GetMedicineByIdQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<MedicineDto>(await _repository.GetById(request.Id, trackChanges: false));
}
