using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OnlineStore.Client.Areas.Identity.Data;

namespace OnlineStore.Client.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("AspNetUsers");

            entity.Property(e => e.Id)
                .HasColumnType("TEXT")
                .IsRequired();

            entity.Property(e => e.FirstName)
                .HasColumnType("TEXT");

            entity.Property(e => e.LastName)
                .HasColumnType("TEXT");

            entity.Property(e => e.DateOfBirth)
                .HasColumnType("TEXT");

            entity.Property(e => e.Address)
                .HasColumnType("TEXT");

            entity.Property(e => e.City)
                .HasColumnType("TEXT");

            entity.Property(e => e.ZipCode)
                .HasColumnType("TEXT");

            entity.Property(e => e.UserName)
                .HasColumnType("TEXT")
                .HasMaxLength(256);

            entity.Property(e => e.NormalizedUserName)
                .HasColumnType("TEXT")
                .HasMaxLength(256);

            entity.Property(e => e.Email)
                .HasColumnType("TEXT")
                .HasMaxLength(256);

            entity.Property(e => e.NormalizedEmail)
                .HasColumnType("TEXT")
                .HasMaxLength(256);

            entity.Property(e => e.EmailConfirmed)
                .HasColumnType("INTEGER")
                .IsRequired();

            entity.Property(e => e.PasswordHash)
                .HasColumnType("TEXT");

            entity.Property(e => e.SecurityStamp)
                .HasColumnType("TEXT");

            entity.Property(e => e.ConcurrencyStamp)
                .HasColumnType("TEXT");

            entity.Property(e => e.PhoneNumber)
                .HasColumnType("TEXT");

            entity.Property(e => e.PhoneNumberConfirmed)
                .HasColumnType("INTEGER")
                .IsRequired();

            entity.Property(e => e.TwoFactorEnabled)
                .HasColumnType("INTEGER")
                .IsRequired();

            entity.Property(e => e.LockoutEnd)
                .HasColumnType("TEXT");

            entity.Property(e => e.LockoutEnabled)
                .HasColumnType("INTEGER")
                .IsRequired();

            entity.Property(e => e.AccessFailedCount)
                .HasColumnType("INTEGER")
                .IsRequired();

            entity.HasKey(e => e.Id);
        });
    }
}

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlite("Data Source=OnlineStore.Client.db");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
