using Microsoft.EntityFrameworkCore;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;

namespace MedicinalSystem.Infrastructure.Repositories;

public class PrescriptionRepository(AppDbContext dbContext) : IPrescriptionRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Prescription entity) => await _dbContext.Prescriptions.AddAsync(entity);

    public async Task<IEnumerable<Prescription>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Prescriptions.Include(e => e.FamilyMember).Include(e => e.Disease).AsNoTracking() 
            : _dbContext.Prescriptions.Include(e => e.FamilyMember).Include(e => e.Disease)).ToListAsync();

    public async Task<Prescription?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Prescriptions.Include(e => e.FamilyMember).Include(e => e.Disease).AsNoTracking() :
            _dbContext.Prescriptions.Include(e => e.FamilyMember).Include(e => e.Disease)).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(Prescription entity) => _dbContext.Prescriptions.Remove(entity);

    public void Update(Prescription entity) => _dbContext.Prescriptions.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

