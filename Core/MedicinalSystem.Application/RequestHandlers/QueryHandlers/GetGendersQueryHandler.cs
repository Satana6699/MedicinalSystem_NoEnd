using MediatR;
using AutoMapper;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers;

public class GetGendersQueryHandler : IRequestHandler<GetGendersQuery, PagedResult<GenderDto>>
{
	private readonly IGenderRepository _repository;
	private readonly IMapper _mapper;

	public GetGendersQueryHandler(IGenderRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<PagedResult<GenderDto>> Handle(GetGendersQuery request, CancellationToken cancellationToken)
    {
        var totalItems = await _repository.CountAsync(request.Name);
        var genders = await _repository.GetPageAsync(request.Page, request.PageSize, request.Name);

        var items = _mapper.Map<IEnumerable<GenderDto>>(genders);
        return new PagedResult<GenderDto>(items, totalItems, request.Page, request.PageSize);
    }
}
