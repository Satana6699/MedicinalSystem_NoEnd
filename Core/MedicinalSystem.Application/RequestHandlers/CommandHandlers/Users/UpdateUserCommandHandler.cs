using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands.Users;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers.Users;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.User.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

        _mapper.Map(request.User, entity);

        _repository.Update(entity);
        await _repository.SaveChanges();

        return true;
    }
}
