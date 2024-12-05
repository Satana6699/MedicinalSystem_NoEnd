using Microsoft.EntityFrameworkCore;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;

namespace MedicinalSystem.Infrastructure.Repositories.SingleRecords;

public class GenderRepository(AppDbContext dbContext) : IGenderRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Gender entity) => await _dbContext.Genders.AddAsync(entity);

    public async Task<IEnumerable<Gender>> Get(bool trackChanges) =>
        await (!trackChanges
            ? _dbContext.Genders.OrderBy(d => d.Id).AsNoTracking()
            : _dbContext.Genders.OrderBy(d => d.Id)).ToListAsync();

    public async Task<Gender?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Genders.AsNoTracking() :
            _dbContext.Genders).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(Gender entity) => _dbContext.Genders.Remove(entity);

    public void Update(Gender entity) => _dbContext.Genders.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();

    public async Task<int> CountAsync(string? name)
    {
        var genders = await _dbContext.Genders.ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            genders = genders.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        return genders.Count();
    }

    public async Task<IEnumerable<Gender>> GetPageAsync(int page, int pageSize, string? name)
    {
        var genders = await _dbContext.Genders.OrderBy(d => d.Id).ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            genders = genders.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return genders.Skip((page - 1) * pageSize).Take(pageSize);
    }
}

