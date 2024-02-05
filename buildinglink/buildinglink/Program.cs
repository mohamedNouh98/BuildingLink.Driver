using BuildingLink.App.Middleware;
using BuildingLink.Core.Database.Contexts;
using BuildingLink.Core.Database.Mappers;
using BuildingLink.Core.Drivers.Mappers;
using BuildingLink.Core.Drivers.Repositories;
using BuildingLink.Infrastructure.Database.Contexts;
using BuildingLink.Infrastructure.Database.Mappers;
using BuildingLink.Infrastructure.Drivers.Mappers;
using BuildingLink.Infrastructure.Drivers.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Db Contexts
builder.Services.AddSingleton<IDbContext, DbContext>();

// Mappers
builder.Services.AddScoped<IDriverMapper, DriverMapper>();
builder.Services.AddScoped<IEntityMapper, EntityMapper>();


// Repositories
builder.Services.AddScoped<IDriverRepository, DriverRepository>();


var app = builder.Build();

// Middlewares
app.UseMiddleware<UnhandledExceptionCatchingMiddleware>();

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
