using Microsoft.EntityFrameworkCore;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;

namespace MedicinalSystem.Infrastructure.Data.Repositories;

public class MedicinePriceRepository(AppDbContext dbContext) : IMedicinePriceRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    private object diseases;

    public async Task Create(MedicinePrice entity) => await _dbContext.MedicinePrices.AddAsync(entity);

    public async Task<IEnumerable<MedicinePrice>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.MedicinePrices.Include(e => e.Medicine).OrderBy(d => d.Id).AsNoTracking() 
            : _dbContext.MedicinePrices.Include(e => e.Medicine).OrderBy(d => d.Id)).ToListAsync();

    public async Task<MedicinePrice?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.MedicinePrices.Include(e => e.Medicine).AsNoTracking() :
            _dbContext.MedicinePrices.Include(e => e.Medicine)).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(MedicinePrice entity) => _dbContext.MedicinePrices.Remove(entity);

    public void Update(MedicinePrice entity) => _dbContext.MedicinePrices.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync(); 
    
    public async Task<int> CountAsync(string? name)
    {
        var medicinePrices = await _dbContext.MedicinePrices.Include(e => e.Medicine).ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            medicinePrices = medicinePrices.Where(s => s.Medicine.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        return medicinePrices.Count();
    }

    public async Task<IEnumerable<MedicinePrice>> GetPageAsync(int page, int pageSize, string? name)
    {
        var medicinePrices = await _dbContext.MedicinePrices.Include(e => e.Medicine).OrderBy(d => d.Id).ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            medicinePrices = medicinePrices.Where(s => s.Medicine.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return medicinePrices.Skip((page - 1) * pageSize).Take(pageSize);
    }
}

