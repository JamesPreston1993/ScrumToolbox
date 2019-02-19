using System.Collections.Generic;
using ScrumToolbox.ProductBacklogs.BacklogItems;

namespace ScrumToolbox.ProductBacklogs.Backlogs
{
    public class ProductBacklog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<BacklogItem> BacklogItems { get; set; }
    }
}
