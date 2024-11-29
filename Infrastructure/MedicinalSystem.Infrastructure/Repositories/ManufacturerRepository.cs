using Microsoft.EntityFrameworkCore;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;

namespace MedicinalSystem.Infrastructure.Data.Repositories;

public class ManufacturerRepository(AppDbContext dbContext) : IManufacturerRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Manufacturer entity) => await _dbContext.Manufacturers.AddAsync(entity);

    public async Task<IEnumerable<Manufacturer>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Manufacturers.OrderBy(d => d.Id).AsNoTracking() 
            : _dbContext.Manufacturers.OrderBy(d => d.Id)).ToListAsync();

    public async Task<Manufacturer?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Manufacturers.AsNoTracking() :
            _dbContext.Manufacturers).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(Manufacturer entity) => _dbContext.Manufacturers.Remove(entity);

    public void Update(Manufacturer entity) => _dbContext.Manufacturers.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync(); 
    
    public async Task<int> CountAsync(string? name)
    {
        var manufacturers = await _dbContext.Diseases.ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            manufacturers = manufacturers.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        return manufacturers.Count();
    }

    public async Task<IEnumerable<Manufacturer>> GetPageAsync(int page, int pageSize, string? name)
    {
        var manufacturers = await _dbContext.Manufacturers.OrderBy(d => d.Id).ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            manufacturers = manufacturers.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return manufacturers.Skip((page - 1) * pageSize).Take(pageSize);
    }
}

