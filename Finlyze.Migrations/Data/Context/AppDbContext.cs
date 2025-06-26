using Finlyze.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Finlyze.Migrations.Data.Context;

public class AppDbContext : DbContext
{
    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<AppLog> AppLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=FinlyzeDb;User Id=sa;Password=Lucas1971!;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

}