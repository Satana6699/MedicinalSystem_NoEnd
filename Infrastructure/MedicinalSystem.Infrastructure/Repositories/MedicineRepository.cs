using Microsoft.EntityFrameworkCore;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;

namespace MedicinalSystem.Infrastructure.Repositories;

public class MedicineRepository(AppDbContext dbContext) : IMedicineRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Medicine entity) => await _dbContext.Medicines.AddAsync(entity);

    public async Task<IEnumerable<Medicine>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Medicines.Include(e => e.Manufacturer).OrderBy(d => d.Id).AsNoTracking() 
            : _dbContext.Medicines.Include(e => e.Manufacturer).OrderBy(d => d.Id)).ToListAsync();

    public async Task<Medicine?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Medicines.Include(e => e.Manufacturer).AsNoTracking() :
            _dbContext.Medicines.Include(e => e.Manufacturer)).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(Medicine entity) => _dbContext.Medicines.Remove(entity);

    public void Update(Medicine entity) => _dbContext.Medicines.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync(); 
    
    public async Task<int> CountAsync(string? name)
    {
        var medicines = await _dbContext.Medicines.ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            medicines = medicines.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        return medicines.Count();
    }

    public async Task<IEnumerable<Medicine>> GetPageAsync(int page, int pageSize, string? name)
    {
        var medicines = await _dbContext.Medicines.Include(e => e.Manufacturer).OrderBy(d => d.Id).ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            medicines = medicines.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return medicines.Skip((page - 1) * pageSize).Take(pageSize);
    }
}

