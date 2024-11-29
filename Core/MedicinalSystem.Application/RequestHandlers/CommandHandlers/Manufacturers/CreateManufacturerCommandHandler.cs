using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands.Manufacturers;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers.Manufacturers;

public class CreateManufacturerCommandHandler : IRequestHandler<CreateManufacturerCommand>
{
    private readonly IManufacturerRepository _repository;
    private readonly IMapper _mapper;

    public CreateManufacturerCommandHandler(IManufacturerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(CreateManufacturerCommand request, CancellationToken cancellationToken)
    {
        await _repository.Create(_mapper.Map<Manufacturer>(request.Manufacturer));
        await _repository.SaveChanges();
    }
}
