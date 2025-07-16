using Finlyze.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finlyze.Migrations.Data.Mapping;

public class TransactionMap : IEntityTypeConfiguration<FinancialTransaction>
{
    public void Configure(EntityTypeBuilder<FinancialTransaction> builder)
    {

        builder.ToTable("FinancialTransaction");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("Int")
            .UseIdentityColumn()
            .ValueGeneratedOnAdd();

        builder.OwnsOne(x => x.Title, title =>
        {
            title.Property(x => x.Value)
                .HasColumnName("Title")
                .HasColumnType("Nvarchar(100)")
                .IsRequired();
        });

        builder.OwnsOne(x => x.Description, descripton =>
        {
            descripton.Property(x => x.Value)
            .HasColumnName("Description")
            .HasColumnType("Nvarchar(300)");
        });

        builder.OwnsOne(x => x.Amount, amount =>
        {
            amount.Property(x => x.Value)
                .HasColumnName("Amount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        });

        builder.OwnsOne(x => x.TranType, type =>
        {
            type.Property(x => x.Value)
                .HasColumnName("TranType")
                .HasColumnType("Int")
                .IsRequired();
        });

        builder.OwnsOne(x => x.CreateAt, create =>
        {
            create.Property(x => x.Value)
                .HasColumnName("CreateAt")
                .HasColumnType("DateTime")
                .IsRequired();
        });

        builder.OwnsOne(x => x.UpdateAt, update =>
        {
            update.Property(x => x.Value)
                .HasColumnName("UpdateAt")
                .HasColumnType("DateTime")
                .IsRequired();
        });

        builder.Property(x => x.UserAccountId)
            .HasColumnName("UserAccountId")
            .IsRequired();

    }
}