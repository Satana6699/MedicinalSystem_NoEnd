using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Dtos.Genders;
using MedicinalSystem.Application.Requests.Queries.Genders;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.Genders;

public class GetGenderByIdQueryHandler : IRequestHandler<GetGenderByIdQuery, GenderDto?>
{
    private readonly IGenderRepository _repository;
    private readonly IMapper _mapper;

    public GetGenderByIdQueryHandler(IGenderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GenderDto?> Handle(GetGenderByIdQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<GenderDto>(await _repository.GetById(request.Id, trackChanges: false));
}
