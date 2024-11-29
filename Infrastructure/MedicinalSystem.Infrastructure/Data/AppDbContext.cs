using Microsoft.EntityFrameworkCore;
using MedicinalSystem.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MedicinalSystem.Domain.Entities;

namespace MedicinalSystem.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Manufacturer> Manufacturers { get; set; }
	public DbSet<Medicine> Medicines { get; set; }
	public DbSet<MedicinePrice> MedicinePrices { get; set; }
	public DbSet<Gender> Genders { get; set; }
	public DbSet<FamilyMember> FamilyMembers { get; set; }
	public DbSet<Disease> Diseases { get; set; }
	public DbSet<Prescription> Prescriptions { get; set; }
	public DbSet<Symptom> Symptoms { get; set; }
	public DbSet<DiseaseSymptom> DiseaseSymptoms { get; set; }
	public DbSet<Treatment> Treatments { get; set; }
    public DbSet<User> Users { get; set; }
}

