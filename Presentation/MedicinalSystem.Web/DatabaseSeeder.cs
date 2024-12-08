using Bogus;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
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
        await _context.Database.MigrateAsync();

        if (!_context.Users.Any()) await CreateUsers();

        if (!_context.Genders.Any()) await CreateGenders();

        if (!_context.FamilyMembers.Any()) await CreateFamilyMembers();

        if (!_context.Manufacturers.Any()) await CreateManufacturers();

        if (!_context.Medicines.Any()) await CreateMedicines();

        if (!_context.Diseases.Any()) await CreateDiseases();

        if (!_context.Treatments.Any()) await CreateTreatments();

        if (!_context.Prescriptions.Any()) await CreatePrescriptions();

        if (!_context.Symptoms.Any()) await CreateSymptoms();

        if (!_context.DiseaseSymptoms.Any()) await CreateDiseaseSymptoms();

        // Сохраняем изменения
        await _context.SaveChangesAsync();
    }

    private async Task CreateDiseaseSymptoms()
    {
        var diseases = _context.Diseases.ToList();
        var symptoms = _context.Symptoms.ToList();

        var existingDiseaseSymptoms = _context.DiseaseSymptoms
            .Select(ds => new { ds.DiseaseId, ds.SymptomId })
            .ToHashSet(); // Создаем коллекцию для отслеживания уже существующих связей

        var diseaseSymptoms = new Faker<DiseaseSymptom>("ru")
            .RuleFor(ds => ds.Id, f => Guid.NewGuid())
            .RuleFor(ds => ds.DiseaseId, f => diseases[f.Random.Int(0, diseases.Count - 1)].Id)
            .RuleFor(ds => ds.SymptomId, f => symptoms[f.Random.Int(0, symptoms.Count - 1)].Id);

        var newDiseaseSymptoms = new List<DiseaseSymptom>();
        var fakerData = diseaseSymptoms.Generate(1000);

        foreach (var ds in fakerData)
        {
            // Проверяем, существует ли уже такая связь
            if (!existingDiseaseSymptoms.Contains(new { ds.DiseaseId, ds.SymptomId }))
            {
                newDiseaseSymptoms.Add(ds);
                existingDiseaseSymptoms.Add(new { ds.DiseaseId, ds.SymptomId });
            }
        }

        await _context.DiseaseSymptoms.AddRangeAsync(newDiseaseSymptoms);
        await _context.SaveChangesAsync();
    }


    private async Task CreateSymptoms()
    {
        var symptoms = new Faker<Symptom>("ru")
            .RuleFor(s => s.Id, f => Guid.NewGuid())
            .RuleFor(s => s.Name, f => f.Lorem.Word() + " " + f.Random.AlphaNumeric(3)); // Уникальность за счёт добавления случайного суффикса

        await _context.Symptoms.AddRangeAsync(symptoms.Generate(50));
        await _context.SaveChangesAsync();
    }

    private async Task CreatePrescriptions()
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

    private async Task CreateTreatments()
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

    private async Task CreateDiseases()
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

    private async Task CreateMedicines()
    {
        var medicinesFaker = new Faker<Medicine>("ru")
            .RuleFor(m => m.Id, f => Guid.NewGuid())
            .RuleFor(m => m.Name, f => f.Commerce.ProductName())
            .RuleFor(m => m.Indications, f => f.Lorem.Sentence())
            .RuleFor(m => m.Contraindications, f => f.Lorem.Sentence())
            .RuleFor(m => m.ManufacturerId, f => _context.Manufacturers.OrderBy(g => Guid.NewGuid()).First().Id)
            .RuleFor(m => m.Packaging, f => f.Commerce.ProductMaterial())
            .RuleFor(m => m.Dosage, f => f.Random.Int(10, 500) + "мг");

        var medicines = medicinesFaker.Generate(200);

        // Создание цен для препаратов
        var medicinePrices = medicines.Select(medicine => new MedicinePrice
        {
            Id = Guid.NewGuid(),
            MedicineId = medicine.Id,
            Price = new Faker().Random.Int(5, 200),
            Date = DateTime.Now.AddDays(-new Faker().Random.Int(1, 365)) // Цена за последний год
        });

        await _context.Medicines.AddRangeAsync(medicines);
        await _context.MedicinePrices.AddRangeAsync(medicinePrices);
        await _context.SaveChangesAsync();
    }

    private async Task CreateManufacturers()
    {
        var manufacturers = new Faker<Manufacturer>("ru")
                        .RuleFor(m => m.Id, f => Guid.NewGuid())
                        .RuleFor(m => m.Name, f => f.Company.CompanyName());

        await _context.Manufacturers.AddRangeAsync(manufacturers.Generate(50));
        await _context.SaveChangesAsync();
    }

    private async Task CreateFamilyMembers()
    {
        var familyMembers = new Faker<FamilyMember>("ru")
                        .RuleFor(f => f.Id, f => Guid.NewGuid())
                        .RuleFor(f => f.Name, f => f.Name.FullName())
                        .RuleFor(f => f.DateOfBirth, f => f.Date.Past(40, DateTime.Now.AddYears(-10)))
                        .RuleFor(f => f.GenderId, f => _context.Genders.OrderBy(g => Guid.NewGuid()).First().Id);

        await _context.FamilyMembers.AddRangeAsync(familyMembers.Generate(200));
        await _context.SaveChangesAsync();
    }

    private async Task CreateUsers()
    {
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
        for (int i = 0; i < 100; i++)
        {
            users.Add(new User
            {
                UserName = "user_" + i,
                FullName = "user_" + i,
                Password = BCrypt.Net.BCrypt.HashPassword("user_" + i),
                PasswordTime = new DateTime(1, 1, 1, 1, 1, 1),
                Role = "user"
            });
        }
        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();
    }

    private async Task CreateGenders()
    {
        await _context.Genders.AddAsync(new Gender() { Id = new Guid(), Name = "Мужской"});
        await _context.Genders.AddAsync(new Gender() { Id = new Guid(), Name = "Женский"});
        await _context.SaveChangesAsync();
    }
}
