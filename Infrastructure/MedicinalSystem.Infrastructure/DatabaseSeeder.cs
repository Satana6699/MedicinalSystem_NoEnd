using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class DatabaseSeeder
{
    private readonly AppDbContext _context;

    public DatabaseSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        // Автоматически применяем миграции (создание базы данных, если отсутствует)
        await _context.Database.MigrateAsync();

        // Проверяем, если ли уже данные
        if (!_context.Diseases.Any())
        {
            var diseases = new List<Disease>
            {
                new Disease { Id = Guid.NewGuid(), Name = "Flu", Duration = "7 days", Symptoms = "Fever, Cough", Consequences = "Weakness" },
                new Disease { Id = Guid.NewGuid(), Name = "Cold", Duration = "5 days", Symptoms = "Sneezing, Runny Nose", Consequences = "None" }
            };

            await _context.Diseases.AddRangeAsync(diseases);
            await _context.SaveChangesAsync();
        }
    }
}
