using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebApplication3.Data;
using WebApplication3.Middleware;
using WebApplication3.Services;

// Create an explicit alias for ASP.NET Core's WebApplication class
using WebApp = Microsoft.AspNetCore.Builder.WebApplication;

var builder = WebApp.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Register Entity Framework Core with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<IStudentService, StudentService>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();