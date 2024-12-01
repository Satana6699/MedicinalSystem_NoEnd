using MediatR;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands.Symptoms;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers.Symptoms;

public class DeleteSymptomCommandHandler(ISymptomRepository repository) : IRequestHandler<DeleteSymptomCommand, bool>
{
    private readonly ISymptomRepository _repository = repository;

    public async Task<bool> Handle(DeleteSymptomCommand request, CancellationToken cancellationToken)
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
