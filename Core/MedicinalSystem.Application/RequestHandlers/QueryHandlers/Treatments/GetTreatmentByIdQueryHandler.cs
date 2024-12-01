using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries.Treatments;
using MedicinalSystem.Application.Dtos.Treatments;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.Treatments;

public class GetUserByIdQueryHandler : IRequestHandler<GetTreatmentByIdQuery, TreatmentDto?>
{
    private readonly ITreatmentRepository _repository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(ITreatmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TreatmentDto?> Handle(GetTreatmentByIdQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<TreatmentDto>(await _repository.GetById(request.Id, trackChanges: false));
}
