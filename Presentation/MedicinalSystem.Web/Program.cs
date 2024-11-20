using AutoMapper;
using MedicinalSystem.Application;
using MedicinalSystem.Web.Extensions;
using MedicinalSystem.Application.Requests.Queries;

var builder = WebApplication.CreateBuilder(args);

// Добавляем поддержку контроллеров (если они нужны)
builder.Services.AddControllers();

// Конфигурируем CORS
builder.Services.ConfigureCors();

// Конфигурируем контекст базы данных
builder.Services.ConfigureDbContext(builder.Configuration);

// Регистрируем дополнительные сервисы
builder.Services.ConfigureServices();

// Конфигурация для AutoMapper
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper autoMapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(autoMapper);

// Добавляем MediatR для обработки запросов и команд
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetManufacturersQuery).Assembly));

// Добавление Swagger для документации
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Настройка Razor Pages
builder.Services.AddRazorPages().AddRazorOptions(options =>
    {
        options.PageViewLocationFormats.Add("/Pages/Shared/{0}.cshtml");
    });
builder.Services.AddScoped<DatabaseSeeder>();  // Если нужно использовать для сеедера данных

var app = builder.Build();

// Обслуживание статических файлов из папки wwwroot
app.UseStaticFiles();  // Эта строка подключает wwwroot

// Вызываем middleware для заполнения базы данных
await app.UseDatabaseSeeder();  // Если необходимо для инициализации данных

// Конфигурация HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Применяем политику CORS
app.UseCors("CorsPolicy");

// Включаем маршрутизацию
app.UseRouting();

// Подключаем контроллеры (если они есть)
app.MapControllers();

// Используем Razor Pages
app.MapRazorPages();

app.MapFallbackToPage("/Home/Index"); // Указывает на страницу Home.cshtml как начальную
app.MapGet("/", () => Results.Redirect("/Home/Index"));

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

// Запуск приложения
app.Run();