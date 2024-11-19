using Microsoft.EntityFrameworkCore;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;

namespace MedicinalSystem.Infrastructure.Repositories;

public class DiseaseSymptomRepository(AppDbContext dbContext) : IDiseaseSymptomRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(DiseaseSymptom entity) => await _dbContext.DiseaseSymptoms.AddAsync(entity);

    public async Task<IEnumerable<DiseaseSymptom>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.DiseaseSymptoms.Include(e => e.Disease).Include(e => e.Symptom).AsNoTracking() 
            : _dbContext.DiseaseSymptoms.Include(e => e.Disease).Include(e => e.Symptom)).ToListAsync();

    public async Task<DiseaseSymptom?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.DiseaseSymptoms.Include(e => e.Disease).Include(e => e.Symptom).AsNoTracking() :
            _dbContext.DiseaseSymptoms.Include(e => e.Disease).Include(e => e.Symptom)).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(DiseaseSymptom entity) => _dbContext.DiseaseSymptoms.Remove(entity);

    public void Update(DiseaseSymptom entity) => _dbContext.DiseaseSymptoms.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

