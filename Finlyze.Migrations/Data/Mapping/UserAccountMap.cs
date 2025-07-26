using Finlyze.Domain.Entities.UserAccountEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finlyze.Migrations.Data.Mapping;

public class UserAccountMap : IEntityTypeConfiguration<UserAccount>
{
    public void Configure(EntityTypeBuilder<UserAccount> builder)
    {
        builder.ToTable("UserAccount");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.Name, name =>
        {
            name.Property(x => x.Value)
                .HasColumnName("Name")
                .HasColumnType("Nvarchar(100)")
                .IsRequired();
        });

        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(x => x.Value)
                .HasColumnName("Email")
                .HasColumnType("Nvarchar(254)")
                .IsRequired();

            email.HasIndex(x => x.Value, "Unique_Key_Email_UserAccount")
                .IsUnique();
        });

        builder.OwnsOne(x => x.Password, password =>
        {
            password.Property(x => x.Value)
                .HasColumnName("Password")
                .HasColumnType("Nvarchar(255)")
                .IsRequired();
        });

        builder.OwnsOne(x => x.PhoneNumber, phone =>
        {
            phone.Property(x => x.Value)
                .HasColumnName("PhoneNumber")
                .HasColumnType("Nvarchar(20)")
                .IsRequired();

            phone.HasIndex(x => x.Value, "Unique_Key_PhoneNumber_UserAccount")
                .IsUnique();
        });

        builder.OwnsOne(x => x.BirthDate, birth =>
        {
            birth.Property(x => x.Value)
                .HasColumnName("BirthDate")
                .HasColumnType("Date")
                .IsRequired();
        });

        builder.OwnsOne(x => x.CreateAt, create =>
        {
            create.Property(x => x.Value)
                .HasColumnName("CreateAt")
                .HasColumnType("DateTime")
                .IsRequired();
        });

        builder.OwnsOne(x => x.Active, active =>
        {
            active.Property(x => x.Value)
                .HasColumnName("Active")
                .HasColumnType("Bit")
                .IsRequired();
        });

        builder.OwnsOne(x => x.Role, role =>
        {
            role.Property(x => x.Value)
                .HasColumnName("Role")
                .HasColumnType("Int")
                .IsRequired();
        });

    }
}
