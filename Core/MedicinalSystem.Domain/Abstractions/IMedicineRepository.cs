using MedicinalSystem.Domain.Entities;

namespace MedicinalSystem.Domain.Abstractions;

public interface IMedicineRepository 
{
	Task<IEnumerable<Medicine>> Get(bool trackChanges);
	Task<Medicine?> GetById(Guid id, bool trackChanges);
    Task<Medicine> Create(Medicine entity);
    void Delete(Medicine entity);
    void Update(Medicine entity);
    Task SaveChanges();
    Task<IEnumerable<Medicine>> GetPageAsync(int page, int pageSize, string? name);
    Task<int> CountAsync(string? name);
}

