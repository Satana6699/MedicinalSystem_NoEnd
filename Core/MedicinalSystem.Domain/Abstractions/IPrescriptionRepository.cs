using MedicinalSystem.Domain.Entities;

namespace MedicinalSystem.Domain.Abstractions;

public interface IPrescriptionRepository 
{
	Task<IEnumerable<Prescription>> Get(bool trackChanges);
	Task<Prescription?> GetById(Guid id, bool trackChanges);
    Task Create(Prescription entity);
    void Delete(Prescription entity);
    void Update(Prescription entity);
    Task SaveChanges();
}

