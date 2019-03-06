using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrumToolbox.ProductBacklogs;
using ScrumToolbox.ProductBacklogs.Backlogs;
using ScrumToolbox.WebApp.Models.ProductBacklogs;

namespace ScrumToolbox.WebApp.Controllers
{
    [Route("api/backlogs")]
    public class ProductBacklogController : ControllerBase
    {
        private readonly IProductBacklogContext backlogContext;

        public ProductBacklogController(IProductBacklogContext backlogContext)
        {
            this.backlogContext = backlogContext;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductBacklogDto productBacklogDto)
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

        [Route("{productBacklogId}")]
        [HttpGet]
        public IActionResult Get(int productBacklogId)
        {
            var productBacklog = this.backlogContext
                .ProductBacklogs
                .Include(b => b.BacklogItems)
                .SingleOrDefault(pb => pb.Id == productBacklogId);

            if (productBacklog == null)
                return BadRequest("Could not find product backlog with specified id.");

            return Ok(productBacklog);
        }

        [HttpGet]
        public IActionResult GetMany()
        {
            var productBacklogs = this.backlogContext
                .ProductBacklogs
                .Include(b => b.BacklogItems)
                .ToList();

            return Ok(productBacklogs);
        }

        [Route("{productBacklogId}")]
        [HttpPatch]
        public IActionResult Update(int productBacklogId, [FromBody] ProductBacklogDto productBacklogDto)
        {
            var productBacklog = this.backlogContext
                .ProductBacklogs
                .SingleOrDefault(pb => pb.Id == productBacklogId);

            if (productBacklog == null)
                return BadRequest("Could not find product backlog with specified id.");

            if (!string.IsNullOrWhiteSpace(productBacklogDto.Name))
                productBacklog.Name = productBacklogDto.Name;

            this.backlogContext.SaveChanges();

            return Ok(productBacklog);
        }

        [Route("{productBacklogId}")]
        [HttpDelete]
        public IActionResult Delete(int productBacklogId)
        {
            var productBacklog = this.backlogContext
                .ProductBacklogs
                .SingleOrDefault(pb => pb.Id == productBacklogId);

            if (productBacklog == null)
                return BadRequest("Could not find product backlog with specified id.");

            this.backlogContext
                .ProductBacklogs
                .Remove(productBacklog);
            this.backlogContext.SaveChanges();

            return NoContent();
        }
    }
}
