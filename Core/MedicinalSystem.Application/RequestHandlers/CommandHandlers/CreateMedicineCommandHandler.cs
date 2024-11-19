﻿using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers;

public class CreateMedicineCommandHandler : IRequestHandler<CreateMedicineCommand>
{
	private readonly IMedicineRepository _repository;
	private readonly IMapper _mapper;

	public CreateMedicineCommandHandler(IMedicineRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateMedicineCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<Medicine>(request.Medicine));
		await _repository.SaveChanges();
	}
}
