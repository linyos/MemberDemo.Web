using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MemberDemo.Web.Models;

namespace MemberDemo.Web.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<MemberProfile> MemberProfiles => Set<MemberProfile>();
    public DbSet<MembershipTier> MembershipTiers => Set<MembershipTier>();
    public DbSet<PointTransaction> PointTransactions => Set<PointTransaction>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<MemberProfile>()
            .HasIndex(m => m.UserId)
            .IsUnique();
    }
}
