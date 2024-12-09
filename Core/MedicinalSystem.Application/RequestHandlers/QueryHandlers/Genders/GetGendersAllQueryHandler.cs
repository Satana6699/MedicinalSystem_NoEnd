using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Application.Dtos.Genders;
using MedicinalSystem.Application.Requests.Queries.Genders;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.Genders;

public class GetGendersAllQueryHandler : IRequestHandler<GetGendersAllQuery, IEnumerable<GenderDto>>
{
    private readonly IGenderRepository _repository;
    private readonly IMapper _mapper;

    public GetGendersAllQueryHandler(IGenderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public IGenderRepository Repository => _repository;

    public async Task<IEnumerable<GenderDto>> Handle(GetGendersAllQuery request, CancellationToken cancellationToken)=>
        _mapper.Map<IEnumerable<GenderDto>>(await Repository.Get(trackChanges: false));
}
