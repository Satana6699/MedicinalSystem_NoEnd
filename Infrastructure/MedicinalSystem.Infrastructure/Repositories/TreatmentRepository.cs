using Microsoft.EntityFrameworkCore;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;

namespace MedicinalSystem.Infrastructure.Repositories;

public class TreatmentRepository(AppDbContext dbContext) : ITreatmentRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Treatment entity) => await _dbContext.Treatments.AddAsync(entity);

    public async Task<IEnumerable<Treatment>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Treatments.Include(e => e.Disease).Include(e => e.Medicine).OrderBy(d => d.Id).AsNoTracking() 
            : _dbContext.Treatments.Include(e => e.Disease).Include(e => e.Medicine).OrderBy(d => d.Id)).ToListAsync();

    public async Task<Treatment?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Treatments.Include(e => e.Disease).Include(e => e.Medicine).AsNoTracking() :
            _dbContext.Treatments.Include(e => e.Disease).Include(e => e.Medicine)).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(Treatment entity) => _dbContext.Treatments.Remove(entity);

    public void Update(Treatment entity) => _dbContext.Treatments.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync(); 
    
    public async Task<int> CountAsync(string? name)
    {
        var treatments = await _dbContext.Treatments.Include(e => e.Disease).Include(e => e.Medicine).ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            treatments = treatments.Where(s => s.Disease.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        return treatments.Count();
    }

    public async Task<IEnumerable<Treatment>> GetPageAsync(int page, int pageSize, string? name)
    {
        var treatments = await _dbContext.Treatments.Include(e => e.Disease).Include(e => e.Medicine).OrderBy(d => d.Id).ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            treatments = treatments.Where(s => s.Disease.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return treatments.Skip((page - 1) * pageSize).Take(pageSize);
    }
}

