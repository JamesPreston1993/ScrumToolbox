using System.Collections.Generic;
using ScrumToolbox.ProductBacklogs.Tasks;

namespace ScrumToolbox.ProductBacklogs.BacklogItems
{
    public class BacklogItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<Task> Tasks { get; set; }
        public BacklogItemProgressState ProgressState { get; set; }
        public int ProductBacklogId { get; set; }
    }
}
