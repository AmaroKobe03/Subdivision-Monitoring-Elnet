using ElnetSubdivi.Models;
using Microsoft.EntityFrameworkCore;
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

    }
}
