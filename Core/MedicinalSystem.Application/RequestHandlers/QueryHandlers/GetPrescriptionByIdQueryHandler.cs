using MediatR;
using AutoMapper;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers;

public class GetPrescriptionByIdQueryHandler : IRequestHandler<GetPrescriptionByIdQuery, PrescriptionDto?>
{
	private readonly IPrescriptionRepository _repository;
	private readonly IMapper _mapper;

	public GetPrescriptionByIdQueryHandler(IPrescriptionRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<PrescriptionDto?> Handle(GetPrescriptionByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<PrescriptionDto>(await _repository.GetById(request.Id, trackChanges: false));
}
