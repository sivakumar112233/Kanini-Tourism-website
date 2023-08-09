using Microsoft.EntityFrameworkCore;
using Toursimtestimonyservice.Interfaces;
using Toursimtestimonyservice.Models;
using Toursimtestimonyservice.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Configuration")));
builder.Services.AddScoped<IRepo<int, FeedBack>, FeedBackRepository>();
builder.Services.AddScoped<IFeedBackService, FeedBackService>();

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("ReactCors", policy =>
    {
        policy.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});
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
