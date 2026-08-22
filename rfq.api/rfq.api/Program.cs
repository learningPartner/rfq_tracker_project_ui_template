using Microsoft.EntityFrameworkCore;
using rfq.api.Data;
using rfq.api.Middleware;
using rfq.api.Repositories;
using rfq.api.Repositories.Interfaces;
using rfq.api.Services;
using rfq.api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repositories
builder.Services.AddScoped<IRfqPortalRfqRepository, RfqPortalRfqRepository>();

// Register Services
builder.Services.AddScoped<IRfqPortalRfqService, RfqPortalRfqService>();

builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

// Use custom exception handling middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Enable Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
