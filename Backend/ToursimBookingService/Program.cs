using Microsoft.EntityFrameworkCore;
using ToursimBookingService.Interfaces;
using ToursimBookingService.Models;
using ToursimBookingService.Repositories;
using ToursimBookingService.Services;

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

builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Configuration"));
});
#region
builder.Services.AddScoped<IRepo<int, Booking>, BookingRepository>();
builder.Services.AddScoped<IToursimBooking, TourBookingService>();
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
