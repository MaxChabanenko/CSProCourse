using Logistic.Core;
using Logistic.DAL;
using Logistic.Models;
using static Logistic.WebAPI.Controllers.ReportController;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<VehicleService>();
builder.Services.AddScoped<WarehouseService>();

//сінглтон адже нам треба працювати з одним _VehicleRepository, апдейт наприклад ніколи не знайде потрібний елемент
builder.Services.AddSingleton<IRepository<Vehicle>, InMemoryRepository<Vehicle>>();
builder.Services.AddSingleton<IRepository<Warehouse>, InMemoryRepository<Warehouse>>();

builder.Services.AddSingleton<ReportService<Vehicle, int>>();

builder.Services.AddSingleton<JsonRepository<Vehicle, int>>();
builder.Services.AddSingleton<XmlRepository<Vehicle, int>>();

builder.Services.AddSingleton<IDictionary<ReportType, IReportingRepository<Vehicle, int>>>(sp =>
    new Dictionary<ReportType, IReportingRepository<Vehicle, int>>
    {
        { ReportType.xml, sp.GetRequiredService<XmlRepository<Vehicle, int>>() },
        { ReportType.json, sp.GetRequiredService<JsonRepository<Vehicle, int>>() },
    });

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
