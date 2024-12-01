using Microsoft.EntityFrameworkCore;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Application.Dtos;
using MedicinalSystem.Application;
using MedicinalSystem.Domain.Abstractions;
using Azure.Core;
using Bogus.DataSets;

namespace MedicinalSystem.Infrastructure.Data.Repositories;

public class SymptomRepository(AppDbContext dbContext) : ISymptomRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    public async Task Create(Symptom entity) => await _dbContext.Symptoms.AddAsync(entity);

    public async Task<IEnumerable<Symptom>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Symptoms.OrderBy(d => d.Id).AsNoTracking() 
            : _dbContext.Symptoms.OrderBy(d => d.Id)).ToListAsync();

    public async Task<Symptom?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Symptoms.AsNoTracking() :
            _dbContext.Symptoms).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(Symptom entity) => _dbContext.Symptoms.Remove(entity);

    public void Update(Symptom entity) => _dbContext.Symptoms.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();

    public async Task<int> CountAsync(string? name)
    {
        var symptoms = await _dbContext.Symptoms.ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            symptoms = symptoms.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        return symptoms.Count();
    }

    public async Task<IEnumerable<Symptom>> GetPageAsync(int page, int pageSize, string? name)
    {
        var symptoms = await _dbContext.Symptoms.OrderBy(d => d.Id).ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            symptoms = symptoms.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        
        return symptoms.Skip((page - 1) * pageSize)
            .Take(pageSize);
    }
}

