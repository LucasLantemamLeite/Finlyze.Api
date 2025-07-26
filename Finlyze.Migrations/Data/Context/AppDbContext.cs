using Finlyze.Domain.Entities;
using Finlyze.Domain.Entities.FinancialTransactionEntity;
using Finlyze.Domain.Entities.SystemLogEntity;
using Finlyze.Domain.Entities.UserAccountEntity;
using Microsoft.EntityFrameworkCore;

namespace Finlyze.Migrations.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
    public DbSet<SystemLog> SystemLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

}