using Microsoft.EntityFrameworkCore;
using TSP.Shared;

namespace TSP.EF
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public virtual DbSet<SubSystem> SubSystems { get; set; }
        public virtual DbSet<SubMenuItem> SubMenuItems { get; set; }
        public virtual DbSet<SubItemDetail> SubItemDetails { get; set; }
        public virtual DbSet<ContactUs> ContactUs { get; set; }
    }
}
