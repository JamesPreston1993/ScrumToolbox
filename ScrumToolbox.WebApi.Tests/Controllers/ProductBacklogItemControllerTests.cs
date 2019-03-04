using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ScrumToolbox.ProductBacklogs;
using ScrumToolbox.ProductBacklogs.BacklogItems;
using ScrumToolbox.TestingUtils.DbContext;
using ScrumToolbox.WebApi.Controllers;
using Xunit;

namespace ScrumToolbox.WebApi.Tests.Controllers
{
    public class ProductBacklogItemControllerTests
    {
        public class GetMany
        {
            private readonly ProductBacklogItemController controller;

            public GetMany()
            {
                var context = new Mock<IProductBacklogContext>();
                context.SetupGet(c => c.BacklogItems)
                    .Returns(DbSetUtils.GetMockDbSet(
                        new BacklogItem { Id = 1, ProductBacklogId = 1},
                        new BacklogItem { Id = 2, ProductBacklogId = 1 },
                        new BacklogItem { Id = 3, ProductBacklogId = 2 }
                    ));
                this.controller = new ProductBacklogItemController(context.Object);
            }

            [Fact]
            public void ReturnsOk()
            {
                // act
                var result = this.controller.GetMany(1);

                // assert
                Assert.Equal(200, ((OkObjectResult)result).StatusCode);
            }

            [Fact]
            public void ReturnsOnlyPbisWithProvidedProductBacklogId()
            {
                // act
                var result = (OkObjectResult) this.controller.GetMany(1);

                // assert
                var pbis = (List<BacklogItem>)result.Value;
                Assert.True(pbis.All(pbi => pbi.ProductBacklogId == 1));
            }
        }
    }
}
