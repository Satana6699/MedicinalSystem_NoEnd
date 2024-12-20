using Microsoft.EntityFrameworkCore;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;
using System.Linq;
namespace MedicinalSystem.Infrastructure.Repositories.SingleRecords;

public class DiseaseRepository(AppDbContext dbContext) : IDiseaseRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Disease entity) => await _dbContext.Diseases.AddAsync(entity);

    public async Task<IEnumerable<Disease>> Get(bool trackChanges) =>
        await (!trackChanges
            ? _dbContext.Diseases.AsNoTracking().OrderBy(d => d.Id)
            : _dbContext.Diseases.OrderBy(d => d.Id)).ToListAsync();

    public async Task<Disease?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Diseases.AsNoTracking() :
            _dbContext.Diseases).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(Disease entity) => _dbContext.Diseases.Remove(entity);

    public void Update(Disease entity) => _dbContext.Diseases.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
    public async Task<IEnumerable<Symptom>> GetSymptomsByDisease(Guid? diseaseId)
    {
        var symptoms = await _dbContext.Set<DiseaseSymptom>()
            .Where(ds => ds.DiseaseId == diseaseId)
            .Select(ds => ds.Symptom)
            .ToListAsync();

        return symptoms;
    }

    public async Task<int> CountAsync(string? name)
    {
        var diseases = await _dbContext.Diseases.ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            diseases = diseases.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        return diseases.Count();
    }

    public async Task<int> CountAsync(List<Guid>? symptomIds)
    {
        var items = await GetDiseasesBySymptomsAsync(symptomIds);
        return items.Count();
    }

    public async Task<IEnumerable<Disease>> GetPageAsync(int page, int pageSize, string? name)
    {
        var diseases = await _dbContext.Diseases.OrderBy(d => d.Id).ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            diseases = diseases.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return diseases.Skip((page - 1) * pageSize).Take(pageSize);
    }

    public async Task<IEnumerable<Disease>> GetPageDiseasesBySymptomsAsync(int page, int pageSize, List<Guid>? symptomIds)
    {
        var results = await GetDiseasesBySymptomsAsync(symptomIds);

        return results.Skip((page - 1) * pageSize).Take(pageSize);
    }

    private async Task<IEnumerable<Disease>> GetDiseasesBySymptomsAsync(List<Guid>? symptomIds)
    {
        symptomIds = symptomIds?.Distinct().ToList();

        if (symptomIds == null || symptomIds.Count == 0)
        {
            var resultsAll = await Get(false);
        }

        var results = await _dbContext.Set<DiseaseSymptom>()
        .Where(ds => symptomIds.Contains(ds.SymptomId))
        .GroupBy(ds => ds.Disease)
        .Where(g => g.Count() == symptomIds.Count)
        .Select(g => g.Key)
        .Distinct()
        .ToListAsync();

        return results;
    }
}
