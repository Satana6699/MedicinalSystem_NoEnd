using MediatR;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Commands.Medicines;

namespace MedicinalSystem.Application.RequestHandlers.CommandHandlers.Medicines;

public class DeleteMedicineCommandHandler(IMedicineRepository repository) : IRequestHandler<DeleteMedicineCommand, bool>
{
    private readonly IMedicineRepository _repository = repository;

    public async Task<bool> Handle(DeleteMedicineCommand request, CancellationToken cancellationToken)
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
