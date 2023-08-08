using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ToursimLoginAndRegistrationService.Interfaces;
using ToursimLoginAndRegistrationService.Models;
using ToursimLoginAndRegistrationService.Repositories;
using ToursimLoginAndRegistrationService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("Configuration"));
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
          ValidateIssuer = false,
          ValidateAudience = false
      };
  });

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITravelAgentService, TravelAgentService>();

builder.Services.AddScoped<IGenerateToken, GenerateTokenService>();
builder.Services.AddScoped<ITravellerService, TravellerService>();

builder.Services.AddScoped<IRepo<int,User>, UsersRepository>();
builder.Services.AddScoped<IRepo<int,TravelAgent>, TravelAgentRepository>();
builder.Services.AddScoped<IRepo<int,Traveller>, TravellerRepository>();
builder.Services.AddScoped<IGenerateToken, GenerateTokenService>();
builder.Services.AddScoped<IRepo<int,Admin>, AdminRepository>();
builder.Services.AddCors(opts =>
{
    opts.AddPolicy("CORS", options =>
    {
        options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CORS");

app.UseAuthentication();



app.MapControllers();

app.Run();
      