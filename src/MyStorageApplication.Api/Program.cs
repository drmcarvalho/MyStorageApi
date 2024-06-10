using Microsoft.OpenApi.Models;
using MyStorageApplication.Database;
using MyStorageApplication.StorageManager.Domain;
using MyStorageApplication.ProductManager.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Repositories/database and services 
builder.Services.AddContainerDatabase();
builder.Services.AddContainerServiceStorageManagerDomain();
builder.Services.AddContainerProductServiceDomain();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyStorage API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
