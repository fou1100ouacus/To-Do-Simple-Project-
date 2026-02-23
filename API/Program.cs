using API.Exceptions;
using Application.Behaviors;
using Application.Common.Interfaces;
using FluentValidation;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
// builder phase (services)
// ├─ AddExceptionHandler<T>
// ├─ AddControllers / AddMinimalApis
// ├─ AddOpenApi / AddEndpointsApiExplorer / AddSwaggerGen
// ├─ AddDbContext + AddScoped<IAppDbContext>
// ├─ AddMediatR
// ├─ AddValidatorsFromAssembly
// ├─ Add pipeline behaviors (Validation, Logging, Performance, …)
// └─ AddAuthentication / AddAuthorization / AddCors / AddRateLimiter …

// app = builder.Build()
  
// app phase (middleware – very strict order!)
// ├─ UseExceptionHandler / UseStatusCodePages
// ├─ UseHsts (production)
// ├─ UseHttpsRedirection
// ├─ UseStaticFiles (if any)
// ├─ UseRouting (usually implicit now)
// ├─ UseAuthentication
// ├─ UseAuthorization
// ├─ UseCors (if not policy-based)
// ├─ MapControllers / MapGroup / MapRazorPages / MapFallback …
// └─ Run()

var builder = WebApplication.CreateBuilder(args);
   
// ────────────────────────────────────────────────
// Services
// ────────────────────────────────────────────────

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();                     // ← Add this line!
// ──── Very important for modern IExceptionHandler ────
builder.Services.AddProblemDetails();                           // ← Add this line!

// Register your handler (singleton lifetime)
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// ──── Database ────
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Data Source=.;Initial Catalog=TodoAppDb2;Integrated Security=True;TrustServerCertificate=True;"
    ));

builder.Services.AddScoped<IAppDbContext, AppDbContext>();

// MediatR + FluentValidation + behaviors
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Application.IAssemblyMarker).Assembly));

builder.Services.AddValidatorsFromAssembly(typeof(Application.IAssemblyMarker).Assembly);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// ────────────────────────────────────────────────
var app = builder.Build();

// ────────────────────────────────────────────────
// Pipeline – order is critical
// ────────────────────────────────────────────────

// 1. Exception handling first
app.UseExceptionHandler();           // ← no lambda needed in most cases

// 2. HTTPS
app.UseHttpsRedirection();

// Development helpers
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();                 // Serves /swagger/v1/swagger.json (or /openapi/v1.json)
    app.UseSwaggerUI();
}

// Main endpoints
app.MapControllers();

app.Run();