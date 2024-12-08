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
    Task<IEnumerable<Symptom>> GetSymptomsByDisease(Guid? diseaseId);
    Task<int> CountAsync(string? name);
    Task<int> CountAsync(List<Guid>? symptomIds);
    Task<IEnumerable<Disease>> GetPageAsync(int page, int pageSize, string? name);
    Task<IEnumerable<Disease>> GetPageDiseasesBySymptomsAsync(int page, int pageSize, List<Guid>? symptomIds);
}

