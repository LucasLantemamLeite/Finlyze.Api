using Finlyze.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finlyze.Migrations.Data.Mapping;

public class TransactionMap : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {

        builder.ToTable("Transaction");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
        .HasColumnName("Id")
        .HasColumnType("Int")
        .UseIdentityColumn()
        .ValueGeneratedOnAdd();

        builder.OwnsOne(x => x.TransactionTitle, title =>
        {
            title.Property(x => x.Value)
                .HasColumnName("TransactionTitle")
                .HasColumnType("Nvarchar(100)")
                .IsRequired();
        });

        builder.OwnsOne(x => x.TransactionDescription, descripton =>
        {
            descripton.Property(x => x.Value)
            .HasColumnName("TransactionDescription")
            .HasColumnType("Nvarchar(300)");
        });

        builder.OwnsOne(x => x.Amount, amount =>
        {
            amount.Property(x => x.Value)
                .HasColumnName("Amount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        });

        builder.OwnsOne(x => x.TypeTransaction, type =>
        {
            type.Property(x => x.Value)
                .HasColumnName("TypeTransaction")
                .HasColumnType("Int")
                .IsRequired();
        });

        builder.OwnsOne(x => x.TransactionCreateAt, create =>
        {
            create.Property(x => x.Value)
                .HasColumnName("TransactionCreateAt")
                .HasColumnType("DateTime")
                .IsRequired();
        });

        builder.OwnsOne(x => x.TransactionUpdateAt, update =>
        {
            update.Property(x => x.Value)
                .HasColumnName("TransactionUpdateAt")
                .HasColumnType("DateTime")
                .IsRequired();
        });

        builder.Property(x => x.UserAccountId)
            .HasColumnName("UserAccountId")
            .IsRequired();

        builder.HasOne(x => x.UserAccount)
            .WithMany(x => x.Transactions)
            .HasForeignKey(x => x.UserAccountId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}