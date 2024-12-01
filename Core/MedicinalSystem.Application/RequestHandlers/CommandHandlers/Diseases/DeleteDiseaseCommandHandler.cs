using MediatR;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands.Diseases;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers.Diseases;

public class DeleteDiseaseCommandHandler(IDiseaseRepository repository) : IRequestHandler<DeleteDiseaseCommand, bool>
{
    private readonly IDiseaseRepository _repository = repository;

    public async Task<bool> Handle(DeleteDiseaseCommand request, CancellationToken cancellationToken)
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
