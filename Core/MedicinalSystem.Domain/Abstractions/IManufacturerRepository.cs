using MedicinalSystem.Domain.Entities;

namespace MedicinalSystem.Domain.Abstractions;

public interface IManufacturerRepository 
{
	Task<IEnumerable<Manufacturer>> Get(bool trackChanges);
	Task<Manufacturer?> GetById(Guid id, bool trackChanges);
    Task Create(Manufacturer entity);
    void Delete(Manufacturer entity);
    void Update(Manufacturer entity);
    Task SaveChanges();
}

