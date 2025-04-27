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
        public DbSet<FacilityOperatingHour> FacilityOperatingHours { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
    }

}
