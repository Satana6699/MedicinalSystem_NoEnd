using Microsoft.EntityFrameworkCore;
using MedicinalSystem.Infrastructure;
using MedicinalSystem.Infrastructure.Repositories;
using MedicinalSystem.Domain.Abstractions;

namespace MedicinalSystem.Web.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("DbConnection"), b =>
                b.MigrationsAssembly("MedicinalSystem.Infrastructure")));
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
		services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
		services.AddScoped<IMedicineRepository, MedicineRepository>();
		services.AddScoped<IMedicinePriceRepository, MedicinePriceRepository>();
		services.AddScoped<IGenderRepository, GenderRepository>();
		services.AddScoped<IFamilyMemberRepository, FamilyMemberRepository>();
		services.AddScoped<IDiseaseRepository, DiseaseRepository>();
		services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
		services.AddScoped<ISymptomRepository, SymptomRepository>();
		services.AddScoped<IDiseaseSymptomRepository, DiseaseSymptomRepository>();
		services.AddScoped<ITreatmentRepository, TreatmentRepository>();
    }
}
