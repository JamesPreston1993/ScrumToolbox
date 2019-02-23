using Microsoft.EntityFrameworkCore;
using ScrumToolbox.ProductBacklogs.BacklogItems;
using ScrumToolbox.ProductBacklogs.Backlogs;
using ScrumToolbox.ProductBacklogs.Tasks;

namespace ScrumToolbox.ProductBacklogs
{
    public interface IProductBacklogContext
    {
        DbSet<ProductBacklog> ProductBacklogs { get; set; }
        DbSet<BacklogItem> BacklogItems { get; set; }
        DbSet<Task> Tasks { get; set; }
        int SaveChanges();
    }
}
