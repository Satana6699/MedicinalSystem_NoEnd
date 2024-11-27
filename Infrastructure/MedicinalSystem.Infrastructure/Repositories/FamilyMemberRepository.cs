using Microsoft.EntityFrameworkCore;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Domain.Abstractions;

namespace MedicinalSystem.Infrastructure.Repositories;

public class FamilyMemberRepository(AppDbContext dbContext) : IFamilyMemberRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(FamilyMember entity) => await _dbContext.FamilyMembers.AddAsync(entity);

    public async Task<IEnumerable<FamilyMember>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.FamilyMembers.Include(e => e.Gender).OrderBy(d => d.Id).AsNoTracking() 
            : _dbContext.FamilyMembers.Include(e => e.Gender).OrderBy(d => d.Id)).ToListAsync();

    public async Task<FamilyMember?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.FamilyMembers.Include(e => e.Gender).AsNoTracking() :
            _dbContext.FamilyMembers.Include(e => e.Gender)).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(FamilyMember entity) => _dbContext.FamilyMembers.Remove(entity);

    public void Update(FamilyMember entity) => _dbContext.FamilyMembers.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();

    public async Task<int> CountAsync(string? name)
    {
        var familyMembers = await _dbContext.FamilyMembers.Include(e => e.Gender).ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            familyMembers = familyMembers.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        return familyMembers.Count();
    }

    public async Task<IEnumerable<FamilyMember>> GetPageAsync(int page, int pageSize, string? name)
    {
        var familyMembers = await _dbContext.FamilyMembers.Include(e => e.Gender).OrderBy(d => d.Id).ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            familyMembers = familyMembers.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return familyMembers.Skip((page - 1) * pageSize).Take(pageSize);
    }
}

