using Microsoft.EntityFrameworkCore;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;

namespace MedicinalSystem.Infrastructure.Repositories;

public class SymptomRepository(AppDbContext dbContext) : ISymptomRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Symptom entity) => await _dbContext.Symptoms.AddAsync(entity);

    public async Task<IEnumerable<Symptom>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Symptoms.AsNoTracking() 
            : _dbContext.Symptoms).ToListAsync();

    public async Task<Symptom?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Symptoms.AsNoTracking() :
            _dbContext.Symptoms).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(Symptom entity) => _dbContext.Symptoms.Remove(entity);

    public void Update(Symptom entity) => _dbContext.Symptoms.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

