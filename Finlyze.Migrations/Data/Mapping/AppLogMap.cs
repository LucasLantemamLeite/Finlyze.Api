using Finlyze.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Finlyze.Migrations.Data.Mapping;

public class AppLogMap : IEntityTypeConfiguration<AppLog>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<AppLog> builder)
    {
        builder.ToTable("AppLog");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
        .HasColumnName("Id")
        .HasColumnType("Int")
        .UseIdentityColumn()
        .ValueGeneratedOnAdd();

        builder.OwnsOne(x => x.LogType, type =>
        {
            type.Property(x => x.Value)
                .HasColumnName("LogType")
                .HasColumnType("Int")
                .IsRequired();
        });

        builder.OwnsOne(x => x.LogTitle, title =>
        {
            title.Property(x => x.Value)
                .HasColumnName("LogTitle")
                .HasColumnType("Nvarchar(100)")
                .IsRequired();
        });

        builder.OwnsOne(x => x.LogDescription, description =>
        {
            description.Property(x => x.Value)
                .HasColumnName("LogDescription")
                .HasColumnType("Nvarchar(200)")
                .IsRequired();
        });

        builder.OwnsOne(x => x.LogCreateAt, create =>
        {
            create.Property(x => x.Value)
                .HasColumnName("LogCreateAt")
                .HasColumnType("DateTime")
                .IsRequired();
        });
    }
}