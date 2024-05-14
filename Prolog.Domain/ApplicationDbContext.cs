using Microsoft.EntityFrameworkCore;
using Prolog.Domain.Abstractions;
using Prolog.Domain.Entities;
using System.Reflection;

namespace Prolog.Domain;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<ActionLog> ActionLogs { get; set; } = null!;
    public DbSet<ExternalSystem> ExternalSystems { get; set; } = null!;
    public DbSet<Driver> Drivers { get; set; } = null!;
    public DbSet<Transport> Transports { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Storage> Storages { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    public DbSet<DriverTransportBind> DriverTransportBinds { get; set; } = null!;
    public DbSet<ProblemSolution> ProblemSolutions { get; set; } = null!;

    public override int SaveChanges()
    {
        UpdateTrackDate();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        UpdateTrackDate();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        UpdateTrackDate();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        UpdateTrackDate();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void UpdateTrackDate()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e is { Entity: IHasTrackDateAttribute, State: EntityState.Added or EntityState.Modified });

        foreach (var entityEntry in entries)
        {
            ((IHasTrackDateAttribute)entityEntry.Entity).DateModified = DateTime.UtcNow;

            if (entityEntry.State == EntityState.Added)
            {
                ((IHasTrackDateAttribute)entityEntry.Entity).DateCreated = DateTime.UtcNow;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).GetTypeInfo().Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }
}