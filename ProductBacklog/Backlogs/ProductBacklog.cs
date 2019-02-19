using System.Collections.Generic;
using ProductBacklog.BacklogItems;

namespace ProductBacklog.Backlogs
{
    public class ProductBacklog
    {
        public string Name { get; set; }
        public IList<BacklogItem> BacklogItems { get; set; }
    }
}
