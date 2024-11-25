using MedicinalSystem.Domain.Entities;

namespace MedicinalSystem.Domain.Abstractions;

public interface IDiseaseSymptomRepository 
{
	Task<IEnumerable<DiseaseSymptom>> Get(bool trackChanges);
	Task<DiseaseSymptom?> GetById(Guid id, bool trackChanges);
    Task Create(DiseaseSymptom entity);
    void Delete(DiseaseSymptom entity);
    void Update(DiseaseSymptom entity);
    Task SaveChanges();
    Task<IEnumerable<DiseaseSymptom>> GetPageAsync(int page, int pageSize, string? nameDisease, string? nameSymptom);
    Task<int> CountAsync(string? nameDisease, string? nameSymptom);
}

