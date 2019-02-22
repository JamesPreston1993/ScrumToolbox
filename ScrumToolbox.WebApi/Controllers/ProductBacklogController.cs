using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ScrumToolbox.ProductBacklogs;
using ScrumToolbox.ProductBacklogs.Backlogs;
using ScrumToolbox.WebApi.Models;

namespace ScrumToolbox.WebApi.Controllers
{
    [Route("api/backlogs")]
    public class ProductBacklogController : ControllerBase
    {
        private readonly ProductBacklogContext backlogContext;

        public ProductBacklogController(ProductBacklogContext backlogContext)
        {
            this.backlogContext = backlogContext;
        }

        [Route("{productBacklogId}")]
        [HttpGet]
        public IActionResult Get(int productBacklogId)
        {
            var productBacklog = this.backlogContext
                .ProductBacklogs
                .SingleOrDefault(pb => pb.Id == productBacklogId);

            if (productBacklog == null)
                return BadRequest("Could not find product backlog with specified id.");

            return Ok(productBacklog);
        }

        [HttpPost]
        public IActionResult Create(ProductBacklogCreationDto productBacklogDto)
        {
            if (string.IsNullOrWhiteSpace(productBacklogDto.Name))
                return BadRequest("Name cannot be empty.");

            var backlog = new ProductBacklog
            {
                Name = productBacklogDto.Name
            };

            this.backlogContext.ProductBacklogs.Add(backlog);
            this.backlogContext.SaveChanges();

            return Created($"{backlog.Id}", backlog);
        }
    }
}
