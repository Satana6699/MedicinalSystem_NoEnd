using MedicinalSystem.Domain.Entities;

namespace MedicinalSystem.Domain.Abstractions;

public interface ITreatmentRepository 
{
	Task<IEnumerable<Treatment>> Get(bool trackChanges);
	Task<Treatment?> GetById(Guid id, bool trackChanges);
    Task Create(Treatment entity);
    void Delete(Treatment entity);
    void Update(Treatment entity);
    Task SaveChanges();
    Task<IEnumerable<Treatment>> GetPageAsync(int page, int pageSize, string? name);
    Task<int> CountAsync(string? name);
}

