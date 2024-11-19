using Microsoft.EntityFrameworkCore;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;

namespace MedicinalSystem.Infrastructure.Repositories;

public class GenderRepository(AppDbContext dbContext) : IGenderRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Gender entity) => await _dbContext.Genders.AddAsync(entity);

    public async Task<IEnumerable<Gender>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Genders.AsNoTracking() 
            : _dbContext.Genders).ToListAsync();

    public async Task<Gender?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Genders.AsNoTracking() :
            _dbContext.Genders).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(Gender entity) => _dbContext.Genders.Remove(entity);

    public void Update(Gender entity) => _dbContext.Genders.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

