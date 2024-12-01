using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands.Genders;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers.Genders;

public class CreateGenderCommandHandler : IRequestHandler<CreateGenderCommand>
{
    private readonly IGenderRepository _repository;
    private readonly IMapper _mapper;

    public CreateGenderCommandHandler(IGenderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(CreateGenderCommand request, CancellationToken cancellationToken)
    {
        await _repository.Create(_mapper.Map<Gender>(request.Gender));
        await _repository.SaveChanges();
    }
}
