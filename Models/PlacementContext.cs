using Microsoft.EntityFrameworkCore;

namespace PlacementDashboard.Models
{
    public class PlacementContext : DbContext
    {
        public PlacementContext(
            DbContextOptions<PlacementContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}