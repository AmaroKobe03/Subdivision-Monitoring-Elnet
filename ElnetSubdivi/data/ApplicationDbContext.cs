using ElnetSubdivi.Models;
using Microsoft.EntityFrameworkCore;
using System;
namespace ElnetSubdivi.data

{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<UserAccount> User_Accounts { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<ServiceRequest> Service_Request { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<FacilityOperatingHour> Facility_Operating_Hours { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<BillingPaymentModel> Billing_Statements { get; set; }
        public DbSet<VisitorList> Visitor_List { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Feedback> Feedback { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FacilityOperatingHour>()
                .HasKey(foh => new { foh.Facility_Id, foh.Day_Of_Week });

            modelBuilder.Entity<FacilityOperatingHour>()
                .HasOne(foh => foh.Facility)
                .WithMany(f => f.OperatingHours) // Fix: Ensure the type of `OperatingHours` matches `ICollection<FacilityOperatingHour>`
                .HasForeignKey(foh => foh.Facility_Id);
        }
    }

}
