using System.Collections.Generic;
using ProductBacklog.Tasks;

namespace ProductBacklog.BacklogItems
{
    public class BacklogItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<Task> Tasks { get; set; }
        public BacklogItemProgressState ProgressState { get; set; }
        public int ProductBacklogId { get; set; }
    }
}
