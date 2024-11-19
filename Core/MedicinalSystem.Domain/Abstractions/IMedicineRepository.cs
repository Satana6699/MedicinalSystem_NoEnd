using MedicinalSystem.Domain.Entities;

namespace MedicinalSystem.Domain.Abstractions;

public interface IMedicineRepository 
{
	Task<IEnumerable<Medicine>> Get(bool trackChanges);
	Task<Medicine?> GetById(Guid id, bool trackChanges);
    Task Create(Medicine entity);
    void Delete(Medicine entity);
    void Update(Medicine entity);
    Task SaveChanges();
}

