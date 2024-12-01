
using MediatR;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands.Genders;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers.Genders;

public class DeleteGenderCommandHandler(IGenderRepository repository) : IRequestHandler<DeleteGenderCommand, bool>
{
    private readonly IGenderRepository _repository = repository;

    public async Task<bool> Handle(DeleteGenderCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id, trackChanges: false);

        if (entity is null)
        {
            return false;
        }

        _repository.Delete(entity);
        await _repository.SaveChanges();

        return true;
    }
}
