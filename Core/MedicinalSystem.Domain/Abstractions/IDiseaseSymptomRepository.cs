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
}

