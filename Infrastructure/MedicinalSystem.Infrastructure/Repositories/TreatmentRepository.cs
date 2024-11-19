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
            ? _dbContext.Treatments.Include(e => e.Disease).Include(e => e.Medicine).AsNoTracking() 
            : _dbContext.Treatments.Include(e => e.Disease).Include(e => e.Medicine)).ToListAsync();

    public async Task<Treatment?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Treatments.Include(e => e.Disease).Include(e => e.Medicine).AsNoTracking() :
            _dbContext.Treatments.Include(e => e.Disease).Include(e => e.Medicine)).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(Treatment entity) => _dbContext.Treatments.Remove(entity);

    public void Update(Treatment entity) => _dbContext.Treatments.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

