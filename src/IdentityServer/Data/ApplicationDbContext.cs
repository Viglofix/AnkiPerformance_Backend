using IdentityServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);// Customize the ASP.NET Identity model and override the defaults if needed.// For example, you can rename the ASP.NET Identity table names and more.// Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<ApplicationUser>()
        .Ignore(x => x.TwoFactorEnabled)
        .Ignore(x => x.AccessFailedCount)
        .Ignore(x => x.ConcurrencyStamp)
        .Ignore(x => x.LockoutEnabled)
        .Ignore(x => x.LockoutEnd)
        .Ignore(x => x.PhoneNumber)
        .Ignore(x => x.PhoneNumberConfirmed)
        .Ignore(x => x.SecurityStamp);

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("asp_net_user");
            entity.Property(x => x.Id)
            .HasColumnName("id");

            entity.Property(x => x.Email)
            .HasColumnName("email");

            entity.Property(x => x.EmailConfirmed)
           .HasColumnName("email_confirmed");

            entity.Property(x => x.NormalizedEmail)
            .HasColumnName("normalized_email");

            entity.Property(x => x.UserName)
            .HasColumnName("login");

            entity.Property(x => x.NormalizedUserName)
            .HasColumnName("normalized_login");
            
            entity.Property(x => x.PasswordHash)
            .HasColumnName("password_hash");
        });
    }    
}
