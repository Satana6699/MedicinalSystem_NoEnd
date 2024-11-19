using MediatR;
using AutoMapper;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers;

public class GetPrescriptionsQueryHandler : IRequestHandler<GetPrescriptionsQuery, IEnumerable<PrescriptionDto>>
{
	private readonly IPrescriptionRepository _repository;
	private readonly IMapper _mapper;

	public GetPrescriptionsQueryHandler(IPrescriptionRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<PrescriptionDto>> Handle(GetPrescriptionsQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<PrescriptionDto>>(await _repository.Get(trackChanges: false));
}
