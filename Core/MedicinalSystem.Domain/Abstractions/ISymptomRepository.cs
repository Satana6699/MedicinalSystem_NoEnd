using MedicinalSystem.Domain.Entities;

namespace MedicinalSystem.Domain.Abstractions;

public interface ISymptomRepository 
{
	Task<IEnumerable<Symptom>> Get(bool trackChanges);
	Task<Symptom?> GetById(Guid id, bool trackChanges);
    Task Create(Symptom entity);
    void Delete(Symptom entity);
    void Update(Symptom entity);
    Task SaveChanges();
}

