using MedicinalSystem.Domain.Entities;

namespace MedicinalSystem.Domain.Abstractions;

public interface IMedicinePriceRepository 
{
	Task<IEnumerable<MedicinePrice>> Get(bool trackChanges);
	Task<MedicinePrice?> GetById(Guid id, bool trackChanges);
    Task Create(MedicinePrice entity);
    void Delete(MedicinePrice entity);
    void Update(MedicinePrice entity);
    Task SaveChanges();
    Task<IEnumerable<MedicinePrice>> GetPageAsync(int page, int pageSize, string? name);
    Task<int> CountAsync(string? name);
}

