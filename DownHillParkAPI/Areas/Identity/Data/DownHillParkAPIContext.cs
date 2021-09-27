using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<User>()
                .HasOne(t => t.Team)
                .WithMany(u => u.Users);
            builder.Entity<User>()
                .HasMany(u => u.Bikes)
                .WithOne(b => b.User);
            builder.Entity<User>()
                .HasOne(c => c.CurrentCompetition)
                .WithMany(u => u.Participants);
            builder.Entity<Competition>()
                .HasOne(p => p.Prize)
                .WithOne(c => c.Competition)
                .HasForeignKey<CompetitionPrize>(p => p.CompetitionId);


        }
    }
}
