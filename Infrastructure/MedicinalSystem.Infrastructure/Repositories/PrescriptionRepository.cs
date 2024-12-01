using Microsoft.EntityFrameworkCore;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;

namespace MedicinalSystem.Infrastructure.Data.Repositories;

public class PrescriptionRepository(AppDbContext dbContext) : IPrescriptionRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Prescription entity) => await _dbContext.Prescriptions.AddAsync(entity);

    public async Task<IEnumerable<Prescription>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Prescriptions.Include(e => e.FamilyMember).Include(e => e.Disease).OrderBy(d => d.Id).AsNoTracking() 
            : _dbContext.Prescriptions.Include(e => e.FamilyMember).Include(e => e.Disease).OrderBy(d => d.Id)).ToListAsync();

    public async Task<Prescription?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Prescriptions.Include(e => e.FamilyMember).Include(e => e.Disease).AsNoTracking() :
            _dbContext.Prescriptions.Include(e => e.FamilyMember).Include(e => e.Disease)).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(Prescription entity) => _dbContext.Prescriptions.Remove(entity);

    public void Update(Prescription entity) => _dbContext.Prescriptions.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync(); 
    
    public async Task<int> CountAsync(string? name)
    {
        var prescriptions = await _dbContext.Prescriptions.Include(e => e.FamilyMember).Include(e => e.Disease).OrderBy(d => d.Id).ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            prescriptions = prescriptions.Where(s => s.FamilyMember.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        return prescriptions.Count();
    }

    public async Task<IEnumerable<Prescription>> GetPageAsync(int page, int pageSize, string? name)
    {
        var prescriptions = await _dbContext.Prescriptions.Include(e => e.FamilyMember).Include(e => e.Disease).OrderBy(d => d.Id).ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            prescriptions = prescriptions.Where(s => s.FamilyMember.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return prescriptions.Skip((page - 1) * pageSize).Take(pageSize);
    }
}

