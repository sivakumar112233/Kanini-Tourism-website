using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using ToursimPackageService.Interfaces;
using ToursimPackageService.Model;
using ToursimPackageService.Models;
using ToursimPackageService.Repositories;
using ToursimPackageService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(opts =>
{
    opts.AddPolicy("ReactCors", policy =>
    {
        policy.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region
//connecting to database
builder.Services.AddDbContext<Context>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Configuration")));
#endregion
#region
//adding all dependency injection to prodiver


builder.Services.AddScoped<IRepo<int, Tours>, ToursRepository>();
builder.Services.AddScoped<IRepo<int,Inclusion>, InclusionRepository>();
builder.Services.AddScoped<IRepo<int,TotalDaysDescription>, TotalDaysDescriptionRepository>();
builder.Services.AddScoped<IServices, TourServices>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ReactCors");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();