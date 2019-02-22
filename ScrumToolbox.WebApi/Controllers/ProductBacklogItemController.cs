using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ScrumToolbox.ProductBacklogs;
using ScrumToolbox.ProductBacklogs.BacklogItems;
using ScrumToolbox.WebApi.Models;

namespace ScrumToolbox.WebApi.Controllers
{
    [Route("api/backlogs/{productBacklogId}/pbis")]
    public class ProductBacklogItemController : ControllerBase
    {
        private readonly ProductBacklogContext backlogContext;

        public ProductBacklogItemController(ProductBacklogContext backlogContext)
        {
            this.backlogContext = backlogContext;
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
