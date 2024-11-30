using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Application.Dtos.Symptoms;
using MedicinalSystem.Application.Requests.Queries.Symptoms;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.Symptoms;

public class GetUsersAllQueryHandler : IRequestHandler<GetSymptomsAllQuery, IEnumerable<SymptomDto>>
{
    private readonly ISymptomRepository _repository;
    private readonly IMapper _mapper;

    public GetUsersAllQueryHandler(ISymptomRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SymptomDto>> Handle(GetSymptomsAllQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<IEnumerable<SymptomDto>>(await _repository.Get(trackChanges: false));
}
