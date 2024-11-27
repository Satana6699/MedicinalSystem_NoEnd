using MedicinalSystem.Domain.Entities;

namespace MedicinalSystem.Domain.Abstractions;

public interface IGenderRepository 
{
	Task<IEnumerable<Gender>> Get(bool trackChanges);
	Task<Gender?> GetById(Guid id, bool trackChanges);
    Task Create(Gender entity);
    void Delete(Gender entity);
    void Update(Gender entity);
    Task SaveChanges();
    Task<IEnumerable<Gender>> GetPageAsync(int page, int pageSize, string? name);
    Task<int> CountAsync(string? name);
}

