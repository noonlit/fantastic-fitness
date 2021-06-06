using Ffitness.Models;
using Ffitness.Models.Stats;
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

        public DbSet<BookedScheduledActivity> BookedScheduledActivities { get; set; }

        public DbSet<Models.UserActions.Booking> UserActionsBooking { get; set; }


        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ScheduledActivity>().Property(s => s.ActivityId).IsRequired();
            modelBuilder.Entity<ScheduledActivity>().Property(s => s.TrainerId).IsRequired();
            modelBuilder.Entity<ScheduledActivity>().Property(s => s.StartTime).IsRequired();
            modelBuilder.Entity<ScheduledActivity>().Property(s => s.EndTime).IsRequired();
            modelBuilder.Entity<ScheduledActivity>().Property(s => s.Price).IsRequired();
            modelBuilder.Entity<ScheduledActivity>().Property(s => s.Capacity).IsRequired();

            modelBuilder.Entity<Booking>()
                .HasIndex(b => new { b.UserId, b.ScheduledActivityId })
                .IsUnique();

            modelBuilder.Entity<UserRole>()
                .HasData(new UserRole { Name = UserRole.ROLE_USER, NormalizedName = UserRole.ROLE_USER.ToUpper() },
                         new UserRole { Name = UserRole.ROLE_ADMIN, NormalizedName = UserRole.ROLE_ADMIN.ToUpper() });

            modelBuilder.Entity<BookedScheduledActivity>().HasNoKey().ToView(null);

            modelBuilder.Entity<Activity>().Property(a => a.Id).IsRequired();
            modelBuilder.Entity<Activity>().Property(a => a.Name).IsRequired();
            modelBuilder.Entity<Activity>().Property(a => a.Description).IsRequired();
            modelBuilder.Entity<Activity>().Property(a => a.Type).IsRequired();
            modelBuilder.Entity<Activity>().Property(a => a.DifficultyLevel).IsRequired();
            modelBuilder.Entity<Activity>().Property(a => a.PrimaryColour).IsRequired();
            modelBuilder.Entity<Activity>().Property(a => a.SecondaryColour).IsRequired();
            modelBuilder.Entity<Activity>().Property(a => a.ActivityPicture).HasDefaultValue("default-activity-picture.jpg");
        }
    }
}
