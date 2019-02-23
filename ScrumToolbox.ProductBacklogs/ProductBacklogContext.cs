using Microsoft.EntityFrameworkCore;
using ScrumToolbox.ProductBacklogs.BacklogItems;
using ScrumToolbox.ProductBacklogs.Backlogs;
using ScrumToolbox.ProductBacklogs.Tasks;

namespace ScrumToolbox.ProductBacklogs
{
    public class ProductBacklogContext : DbContext
    {
        public DbSet<ProductBacklog> ProductBacklogs { get; set; }
        public DbSet<BacklogItem> BacklogItems { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public ProductBacklogContext(DbContextOptions<ProductBacklogContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductBacklog>()
                .HasMany(x => x.BacklogItems)
                .WithOne();
        }
    }
}
