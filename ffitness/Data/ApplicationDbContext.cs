using Ffitness.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<ScheduledActivity> ScheduledActivities { get; set; }

        public DbSet<UserRole> Roles { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>()
                .HasIndex(b => new { b.UserId, b.ScheduledActivityId })
                .IsUnique();
            modelBuilder.Entity<UserRole>()
                .HasData(new UserRole { Name = "User" },
                         new UserRole { Name = "Admin" });
        }
    }
}
