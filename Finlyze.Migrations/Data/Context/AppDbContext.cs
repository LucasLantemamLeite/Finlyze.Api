using Finlyze.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Finlyze.Migrations.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<FinancialTransaction> Transactions { get; set; }
    public DbSet<SystemLog> AppLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

}