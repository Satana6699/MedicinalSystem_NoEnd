using Microsoft.EntityFrameworkCore;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;

namespace MedicinalSystem.Infrastructure.Repositories;

public class DiseaseRepository(AppDbContext dbContext) : IDiseaseRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Disease entity) => await _dbContext.Diseases.AddAsync(entity);

    public async Task<IEnumerable<Disease>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Diseases.AsNoTracking() 
            : _dbContext.Diseases).ToListAsync();

    public async Task<Disease?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Diseases.AsNoTracking() :
            _dbContext.Diseases).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(Disease entity) => _dbContext.Diseases.Remove(entity);

    public void Update(Disease entity) => _dbContext.Diseases.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
    public async Task<int> CountAsync(string? name)
    {
        var diseases = await _dbContext.Diseases.ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            diseases = diseases.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        return diseases.Count();
    }

    public async Task<IEnumerable<Disease>> GetPageAsync(int page, int pageSize, string? name)
    {
        var diseases = await _dbContext.Diseases.ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            diseases = diseases.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return diseases.Skip((page - 1) * pageSize).Take(pageSize);
    }
}

