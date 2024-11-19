using Microsoft.EntityFrameworkCore;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;

namespace MedicinalSystem.Infrastructure.Repositories;

public class ManufacturerRepository(AppDbContext dbContext) : IManufacturerRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Manufacturer entity) => await _dbContext.Manufacturers.AddAsync(entity);

    public async Task<IEnumerable<Manufacturer>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Manufacturers.AsNoTracking() 
            : _dbContext.Manufacturers).ToListAsync();

    public async Task<Manufacturer?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Manufacturers.AsNoTracking() :
            _dbContext.Manufacturers).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(Manufacturer entity) => _dbContext.Manufacturers.Remove(entity);

    public void Update(Manufacturer entity) => _dbContext.Manufacturers.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

