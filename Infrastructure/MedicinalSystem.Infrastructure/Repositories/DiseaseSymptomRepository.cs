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
            ? _dbContext.DiseaseSymptoms.Include(e => e.Disease).Include(e => e.Symptom).OrderBy(d => d.Id).AsNoTracking() 
            : _dbContext.DiseaseSymptoms.Include(e => e.Disease).Include(e => e.Symptom).OrderBy(d => d.Id)).ToListAsync();

    public async Task<DiseaseSymptom?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.DiseaseSymptoms.Include(e => e.Disease).Include(e => e.Symptom).AsNoTracking() :
            _dbContext.DiseaseSymptoms.Include(e => e.Disease).Include(e => e.Symptom)).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(DiseaseSymptom entity) => _dbContext.DiseaseSymptoms.Remove(entity);

    public void Update(DiseaseSymptom entity) => _dbContext.DiseaseSymptoms.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
    public async Task<int> CountAsync(string? nameDisease, string? nameSymptom)
    {
        var diseaseSymptoms = await _dbContext.DiseaseSymptoms.Include(d => d.Disease).Include(s => s.Symptom).ToListAsync();
        if (!string.IsNullOrWhiteSpace(nameDisease))
        {
            diseaseSymptoms = diseaseSymptoms.Where(s => s.Disease.Name.Contains(nameDisease, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        if (!string.IsNullOrWhiteSpace(nameSymptom))
        {
            diseaseSymptoms = diseaseSymptoms.Where(s => s.Symptom.Name.Contains(nameSymptom, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        return diseaseSymptoms.Count();
    }

    public async Task<IEnumerable<DiseaseSymptom>> GetPageAsync(int page, int pageSize, string? nameDisease, string? nameSymptom)
    {
        var diseaseSymptoms = await _dbContext.DiseaseSymptoms.Include(d => d.Disease).Include(s => s.Symptom).OrderBy(d => d.Id).ToListAsync();

        if (!string.IsNullOrWhiteSpace(nameDisease))
        {
            diseaseSymptoms = diseaseSymptoms.Where(s => s.Disease.Name.Contains(nameDisease, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        if (!string.IsNullOrWhiteSpace(nameSymptom))
        {
            diseaseSymptoms = diseaseSymptoms.Where(s => s.Symptom.Name.Contains(nameSymptom, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        diseaseSymptoms = diseaseSymptoms.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return diseaseSymptoms;
    }
}

