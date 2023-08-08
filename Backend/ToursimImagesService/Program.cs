
using Microsoft.EntityFrameworkCore;
using ToursimImagesService.Models;
using Microsoft.Extensions.Azure;
using ToursimImagesService.Interfaces;
using ToursimImagesService.Respositories;
using ToursimImagesService.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(opts =>
{
    opts.AddPolicy("ReactCors", policy =>
    {
        policy.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TourImageContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("Configuration")));
builder.Services.AddScoped<IRepo<int,TourImages>, TourImagesRepository>();
builder.Services.AddScoped<ITourImageService, ToursImageServices>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseCors("ReactCors");

app.UseAuthorization();
app.MapControllers();

app.Run();