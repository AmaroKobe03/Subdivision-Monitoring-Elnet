using ElnetSubdivi.Models;
using Microsoft.EntityFrameworkCore;
namespace ElnetSubdivi.data

{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Users> Users { get; set; }
    }
}
