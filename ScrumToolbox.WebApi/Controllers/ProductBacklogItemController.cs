using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ScrumToolbox.ProductBacklogs;

namespace ScrumToolbox.WebApi.Controllers
{
    [Route("api/backlogs/{productBacklogId}/pbis")]
    public class ProductBacklogItemController : ControllerBase
    {
        private readonly IProductBacklogContext backlogContext;

        public ProductBacklogItemController(IProductBacklogContext backlogContext)
        {
            this.backlogContext = backlogContext;
        }

        [Route("{backlogItemId}")]
        [HttpGet]
        public IActionResult Get(int productBacklogId, int backlogItemId)
        {
            var productBacklogItem = this.backlogContext
                .BacklogItems
                .Where(pbi => pbi.ProductBacklogId == productBacklogId)
                .SingleOrDefault(pbi => pbi.Id == backlogItemId);

            if (productBacklogItem == null)
                return BadRequest("Could not find backlog item with specified id and/or backlog id.");

            return Ok(productBacklogItem);
        }

        [HttpGet]
        public IActionResult GetMany(int productBacklogId)
        {
            var pbis = this.backlogContext
                .BacklogItems
                .Where(pbi => pbi.ProductBacklogId == productBacklogId)
                .ToList();

            return Ok(pbis);
        }
    }
}
