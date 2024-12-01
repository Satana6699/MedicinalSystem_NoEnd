using MediatR;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands.Treatments;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers.Treatments;

public class DeleteTreatmentCommandHandler(ITreatmentRepository repository) : IRequestHandler<DeleteTreatmentCommand, bool>
{
    private readonly ITreatmentRepository _repository = repository;

    public async Task<bool> Handle(DeleteTreatmentCommand request, CancellationToken cancellationToken)
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
