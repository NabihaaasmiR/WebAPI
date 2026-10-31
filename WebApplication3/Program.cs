using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore; // <-- Required using directive for Scalar
using WebApplication3.Data;
using WebApplication3.Middleware;
using WebApplication3.Repositories;
using WebApplication3.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// 1. Register the built-in OpenAPI generator
builder.Services.AddOpenApi();

// Database Context Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository & Service Registrations
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

// Global Error Handling & Problem Details
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler(); // Activates your GlobalExceptionHandler

if (app.Environment.IsDevelopment())
{
    // 2. Map the OpenAPI JSON endpoint
    app.MapOpenApi();

    // 3. Map Scalar API Reference UI (replaces UseSwaggerUI)
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Student Management API")
               .WithTheme(ScalarTheme.Mars); // Optional: choose your preferred theme
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();