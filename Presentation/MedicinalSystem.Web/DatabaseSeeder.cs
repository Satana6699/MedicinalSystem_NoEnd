using Bogus;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using MedicinalSystem.Application.Dtos.Users;
using Microsoft.Win32;
using Gender = MedicinalSystem.Domain.Entities.Gender;
using static Bogus.DataSets.Name;

public class DatabaseSeeder
{
    private readonly AppDbContext _context;

    public DatabaseSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        // Автоматически применяем миграции
        await _context.Database.MigrateAsync();

        // Генерация данных
        if (!_context.Users.Any())
        {
            // Создаём объект пользователя
            var admin = new User
            {
                UserName = "admin",
                FullName = "admin",
                Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                PasswordTime = new DateTime(1, 1, 1, 1, 1, 1),
                Role = "Admin"
            };
            await _context.Users.AddAsync(admin);

            List<User> users = new List<User>();
            for (int i = 0; i < 100;  i++)
            {
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword("user_" + i);
                // Создаём объект пользователя
                var user = new User
                {
                    UserName = "user_" + i,
                    FullName = "user_" + i,
                    Password = hashedPassword,
                    PasswordTime = new DateTime(1, 1, 1, 1, 1, 1),
                    Role = "user"
                };
                users.Add(user);
            }
            await _context.Users.AddRangeAsync(users);

            await _context.SaveChangesAsync();
        }
            
            
            // Генерация данных
        if (!_context.Genders.Any())
        {
            var genders = new Faker<Gender>("ru")
                .RuleFor(g => g.Id, f => Guid.NewGuid())
                .RuleFor(g => g.Name, f => f.PickRandom(new[] { "Мужской", "Женский" }));

            await _context.Genders.AddRangeAsync(genders.Generate(2));
            await _context.SaveChangesAsync();
        }

        if (!_context.FamilyMembers.Any())
        {
            var familyMembers = new Faker<FamilyMember>("ru")
                .RuleFor(f => f.Id, f => Guid.NewGuid())
                .RuleFor(f => f.Name, f => f.Name.FullName())
                .RuleFor(f => f.DateOfBirth, f => f.Date.Past(40, DateTime.Now.AddYears(-10)))
                .RuleFor(f => f.GenderId, f => _context.Genders.OrderBy(g => Guid.NewGuid()).First().Id);

            await _context.FamilyMembers.AddRangeAsync(familyMembers.Generate(200));
            await _context.SaveChangesAsync();
        }

        if (!_context.Manufacturers.Any())
        {
            var manufacturers = new Faker<Manufacturer>("ru")
                .RuleFor(m => m.Id, f => Guid.NewGuid())
                .RuleFor(m => m.Name, f => f.Company.CompanyName());

            await _context.Manufacturers.AddRangeAsync(manufacturers.Generate(50));
            await _context.SaveChangesAsync();
        }

        if (!_context.Medicines.Any())
        {
            var medicines = new Faker<Medicine>("ru")
                .RuleFor(m => m.Id, f => Guid.NewGuid())
                .RuleFor(m => m.Name, f => f.Commerce.ProductName())
                .RuleFor(m => m.Indications, f => f.Lorem.Sentence())
                .RuleFor(m => m.Contraindications, f => f.Lorem.Sentence())
                .RuleFor(m => m.ManufacturerId, f => _context.Manufacturers.OrderBy(g => Guid.NewGuid()).First().Id)
                .RuleFor(m => m.Packaging, f => f.Commerce.ProductMaterial())
                .RuleFor(m => m.Dosage, f => f.Random.Int(10, 500) + "мг");

            await _context.Medicines.AddRangeAsync(medicines.Generate(200));
            await _context.SaveChangesAsync();
        }

        if (!_context.Diseases.Any())
        {
            var diseases = new Faker<Disease>("ru")
                .RuleFor(d => d.Id, f => Guid.NewGuid())
                .RuleFor(d => d.Name, f => f.Lorem.Word())
                .RuleFor(d => d.Duration, f => f.Random.Int(1, 14) + " дней")
                .RuleFor(d => d.Symptoms, f => f.Lorem.Sentence())
                .RuleFor(d => d.Consequences, f => f.Lorem.Sentence());

            await _context.Diseases.AddRangeAsync(diseases.Generate(200));
            await _context.SaveChangesAsync();
        }

        if (!_context.Treatments.Any())
        {
            var treatments = new Faker<Treatment>("ru")
                .RuleFor(t => t.Id, f => Guid.NewGuid())
                .RuleFor(t => t.DiseaseId, f => _context.Diseases.OrderBy(g => Guid.NewGuid()).First().Id)
                .RuleFor(t => t.MedicineId, f => _context.Medicines.OrderBy(g => Guid.NewGuid()).First().Id)
                .RuleFor(t => t.Dosage, f => f.Random.Int(1, 5) + " таблеток")
                .RuleFor(t => t.DurationDays, f => f.Random.Int(1, 14))
                .RuleFor(t => t.IntervalHours, f => f.Random.Int(6, 12))
                .RuleFor(t => t.Instructions, f => f.Lorem.Sentence());

            await _context.Treatments.AddRangeAsync(treatments.Generate(200));
            await _context.SaveChangesAsync();
        }

        if (!_context.Prescriptions.Any())
        {
            var prescriptions = new Faker<Prescription>("ru")
                .RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.FamilyMemberId, f => _context.FamilyMembers.OrderBy(g => Guid.NewGuid()).First().Id)
                .RuleFor(p => p.DiseaseId, f => _context.Diseases.OrderBy(g => Guid.NewGuid()).First().Id)
                .RuleFor(p => p.Date, f => f.Date.Past(1))
                .RuleFor(p => p.Status, f => f.Random.Bool());

            await _context.Prescriptions.AddRangeAsync(prescriptions.Generate(200));
            await _context.SaveChangesAsync();
        }

        if (!_context.Symptoms.Any())
        {
            var symptoms = new Faker<Symptom>("ru")
                .RuleFor(s => s.Id, f => Guid.NewGuid())
                .RuleFor(s => s.Name, f => f.Lorem.Word());

            await _context.Symptoms.AddRangeAsync(symptoms.Generate(200));
            await _context.SaveChangesAsync();
        }

        if (!_context.DiseaseSymptoms.Any())
        {
            var diseaseSymptoms = new Faker<DiseaseSymptom>("ru")
                .RuleFor(ds => ds.Id, f => Guid.NewGuid())
                .RuleFor(ds => ds.DiseaseId, f => _context.Diseases.OrderBy(g => Guid.NewGuid()).First().Id)
                .RuleFor(ds => ds.SymptomId, f => _context.Symptoms.OrderBy(g => Guid.NewGuid()).First().Id);

            await _context.DiseaseSymptoms.AddRangeAsync(diseaseSymptoms.Generate(200));
            await _context.SaveChangesAsync();
        }

        if (!_context.MedicinePrices.Any())
        {
            var medicinePrices = new Faker<MedicinePrice>("ru")
                .RuleFor(mp => mp.Id, f => Guid.NewGuid())
                .RuleFor(mp => mp.MedicineId, f => _context.Medicines.OrderBy(g => Guid.NewGuid()).First().Id)
                .RuleFor(mp => mp.Price, f => f.Random.Int(5, 200))
                .RuleFor(mp => mp.Date, f => f.Date.Past(1));

            await _context.MedicinePrices.AddRangeAsync(medicinePrices.Generate(200));
            await _context.SaveChangesAsync();
        }

        // Сохраняем изменения
        await _context.SaveChangesAsync();
    }
}
