using Microsoft.EntityFrameworkCore;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;

namespace MedicinalSystem.Infrastructure.Repositories;

public class MedicinePriceRepository(AppDbContext dbContext) : IMedicinePriceRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(MedicinePrice entity) => await _dbContext.MedicinePrices.AddAsync(entity);

    public async Task<IEnumerable<MedicinePrice>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.MedicinePrices.Include(e => e.Medicine).AsNoTracking() 
            : _dbContext.MedicinePrices.Include(e => e.Medicine)).ToListAsync();

    public async Task<MedicinePrice?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.MedicinePrices.Include(e => e.Medicine).AsNoTracking() :
            _dbContext.MedicinePrices.Include(e => e.Medicine)).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(MedicinePrice entity) => _dbContext.MedicinePrices.Remove(entity);

    public void Update(MedicinePrice entity) => _dbContext.MedicinePrices.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

