using MedicinalSystem.Domain.Entities;

namespace MedicinalSystem.Domain.Abstractions;

public interface IDiseaseRepository 
{
	Task<IEnumerable<Disease>> Get(bool trackChanges);
	Task<Disease?> GetById(Guid id, bool trackChanges);
    Task Create(Disease entity);
    void Delete(Disease entity);
    void Update(Disease entity);
    Task SaveChanges();
    Task<IEnumerable<Disease>> GetPageAsync(int page, int pageSize, string? name);
    Task<int> CountAsync(string? name);
}

