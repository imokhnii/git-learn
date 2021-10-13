using DownHillParkAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DownHillParkAPI.Data
{
    public class DownHillParkAPIContext : IdentityDbContext<IdentityUser>
    {
        public DownHillParkAPIContext(DbContextOptions<DownHillParkAPIContext> options)
            : base(options)
        {

        }
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<CompetitionPrize> CompetitionPrizes { get; set; }
        public DbSet<CompetitionResult> CompetitionResults { get; set; }
        public DbSet<Lap> Laps { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<User>()
                .HasOne(u => u.Team)
                .WithMany(t => t.Users);
            builder.Entity<User>()
                .HasMany(u => u.Bikes)
                .WithOne(b => b.User);
            builder.Entity<User>()
                .HasOne(u => u.CurrentCompetition)
                .WithMany(c => c.Participants);
            builder.Entity<Competition>()
                .HasOne(c => c.Prize)
                .WithOne(p => p.Competition)
                .HasForeignKey<CompetitionPrize>(p => p.CompetitionId);
            builder.Entity<Competition>()
                .HasMany(c => c.Results)
                .WithOne(r => r.Competition);
            builder.Entity<CompetitionResult>()
                .HasMany(r => r.Laps)
                .WithOne(l => l.Result);


        }
    }
}
