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
        public class Get
        {
            private readonly ProductBacklogItemController controller;

            public Get()
            {
                var context = new Mock<IProductBacklogContext>();
                context.SetupGet(c => c.BacklogItems)
                    .Returns(DbSetUtils.GetMockDbSet(
                        new BacklogItem { Id = 1, ProductBacklogId = 1 },
                        new BacklogItem { Id = 2, ProductBacklogId = 2 }
                    ));
                this.controller = new ProductBacklogItemController(context.Object);
            }

            [Fact]
            public void ReturnsOk()
            {
                // act
                var result = this.controller.Get(1, 1);

                // assert
                Assert.Equal(200, ((OkObjectResult)result).StatusCode);
            }

            [Fact]
            public void ReturnsBadRequestWhenIdIsInvalid()
            {
                // act
                var result = this.controller.Get(1, 2);

                // assert
                Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);
                Assert.Equal("Could not find backlog item with specified id and/or backlog id.", ((BadRequestObjectResult)result).Value);
            }

            [Fact]
            public void ReturnsBadRequestWhenBacklogIdIsInvalid()
            {
                // act
                var result = this.controller.Get(2, 1);

                // assert
                Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);
                Assert.Equal("Could not find backlog item with specified id and/or backlog id.", ((BadRequestObjectResult)result).Value);
            }
        }

        public class GetMany
        {
            private readonly ProductBacklogItemController controller;

            public GetMany()
            {
                var context = new Mock<IProductBacklogContext>();
                context.SetupGet(c => c.BacklogItems)
                    .Returns(DbSetUtils.GetMockDbSet(
                        new BacklogItem { Id = 1, ProductBacklogId = 1 },
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
                var result = (OkObjectResult)this.controller.GetMany(1);

                // assert
                var pbis = (List<BacklogItem>)result.Value;
                Assert.True(pbis.All(pbi => pbi.ProductBacklogId == 1));
            }
        }

        public class Delete
        {
            private readonly ProductBacklogItemController controller;

            public Delete()
            {
                var context = new Mock<IProductBacklogContext>();
                context.SetupGet(c => c.BacklogItems)
                    .Returns(DbSetUtils.GetMockDbSet(new BacklogItem { Id = 1, ProductBacklogId = 1 }));
                this.controller = new ProductBacklogItemController(context.Object);
            }

            [Fact]
            public void ReturnsNoContent()
            {
                // act
                var result = this.controller.Delete(1, 1);

                // assert
                Assert.Equal(204, ((NoContentResult)result).StatusCode);
            }

            [Fact]
            public void ReturnsBadRequestWhenIdIsInvalid()
            {
                // act
                var result = this.controller.Delete(1, 2);

                // assert
                Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);
                Assert.Equal("Could not find backlog item with specified id and/or backlog id.", ((BadRequestObjectResult)result).Value);
            }

            [Fact]
            public void ReturnsBadRequestWhenBacklogIdIsInvalid()
            {
                // act
                var result = this.controller.Delete(2, 1);

                // assert
                Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);
                Assert.Equal("Could not find backlog item with specified id and/or backlog id.", ((BadRequestObjectResult)result).Value);
            }
        }
    }
}
