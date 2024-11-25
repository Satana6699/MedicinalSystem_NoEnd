using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Domain.Entities;
namespace MedicinalSystem.Domain.Abstractions;

public interface ISymptomRepository 
{
	Task<IEnumerable<Symptom>> Get(bool trackChanges);
    Task<IEnumerable<Symptom>> GetSymptomsAsync(int page, int pageSize);
	Task<Symptom?> GetById(Guid id, bool trackChanges);
    Task Create(Symptom entity);
    void Delete(Symptom entity);
    void Update(Symptom entity);
    Task SaveChanges();
    Task<int> CountAsync();
}

