using Application;
using Infrastructure;
using Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("GooglePlaceApi"));
builder.Services.AddPersistence(builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    Console.WriteLine("Base SQLite à : " + Path.GetFullPath("places.db"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();