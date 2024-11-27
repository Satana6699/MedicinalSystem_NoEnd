using MedicinalSystem.Domain.Entities;

namespace MedicinalSystem.Domain.Abstractions;

public interface IFamilyMemberRepository 
{
	Task<IEnumerable<FamilyMember>> Get(bool trackChanges);
	Task<FamilyMember?> GetById(Guid id, bool trackChanges);
    Task Create(FamilyMember entity);
    void Delete(FamilyMember entity);
    void Update(FamilyMember entity);
    Task SaveChanges();
    Task<IEnumerable<FamilyMember>> GetPageAsync(int page, int pageSize, string? name);
    Task<int> CountAsync(string? name);
}

