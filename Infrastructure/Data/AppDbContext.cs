using Application.Common.Interfaces;
using Domain.Todos;
using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
: DbContext(options), IAppDbContext
{
    public DbSet<Todo> Todos => Set<Todo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoConfiguration).Assembly);   
    }
}