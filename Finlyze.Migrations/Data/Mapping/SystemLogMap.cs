using Finlyze.Domain.Entities.SystemLogEntity;
using Microsoft.EntityFrameworkCore;

namespace Finlyze.Migrations.Data.Mapping;

public class SystemLogMap : IEntityTypeConfiguration<SystemLog>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SystemLog> builder)
    {
        builder.ToTable("SystemLog");

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

        builder.OwnsOne(x => x.Title, title =>
        {
            title.Property(x => x.Value)
                .HasColumnName("Title")
                .HasColumnType("Nvarchar(100)")
                .IsRequired();
        });

        builder.OwnsOne(x => x.Description, description =>
        {
            description.Property(x => x.Value)
                .HasColumnName("Description")
                .HasColumnType("Nvarchar(200)")
                .IsRequired();
        });

        builder.OwnsOne(x => x.CreateAt, create =>
        {
            create.Property(x => x.Value)
                .HasColumnName("CreateAt")
                .HasColumnType("DateTime")
                .IsRequired();
        });
    }
}