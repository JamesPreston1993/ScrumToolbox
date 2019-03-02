using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ScrumToolbox.ProductBacklogs;
using ScrumToolbox.ProductBacklogs.Backlogs;
using ScrumToolbox.TestingUtils.DbContext;
using ScrumToolbox.WebApi.Controllers;
using ScrumToolbox.WebApi.Models.ProductBacklogs;
using Xunit;

namespace ScrumToolbox.WebApi.Tests.Controllers
{
    public class ProductBacklogControllerTests
    {
        public class Create
        {
            private readonly ProductBacklogController controller;

            public Create()
            {
                var context = new Mock<IProductBacklogContext>();
                context.SetupGet(c => c.ProductBacklogs)
                    .Returns(DbSetUtils.GetMockDbSet<ProductBacklog>());
                this.controller = new ProductBacklogController(context.Object);
            }

            [Fact]
            public void ReturnsCreated()
            {
                // act
                var result = this.controller.Create(new ProductBacklogDto
                {
                    Name = "Test"
                });

                // assert
                Assert.Equal(201, ((CreatedResult)result).StatusCode);
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void ReturnsBadRequestWhenNameIsWhitespaceOrEmpty(string name)
            {
                // act
                var result = this.controller.Create(new ProductBacklogDto
                {
                    Name = name
                });

                // assert
                Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);
                Assert.Equal("Name cannot be empty.", ((BadRequestObjectResult)result).Value);
            }
        }

        public class Get
        {
            private readonly ProductBacklogController controller;

            public Get()
            {
                var context = new Mock<IProductBacklogContext>();
                context.SetupGet(c => c.ProductBacklogs)
                    .Returns(DbSetUtils.GetMockDbSet(new ProductBacklog { Id = 1 }));
                this.controller = new ProductBacklogController(context.Object);
            }

            [Fact]
            public void ReturnsOk()
            {
                // act
                var result = this.controller.Get(1);

                // assert
                Assert.Equal(200, ((OkObjectResult)result).StatusCode);
            }

            [Fact]
            public void ReturnsBadRequestWhenIdIsInvalid()
            {
                // act
                var result = this.controller.Get(2);

                // assert
                Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);
            }
        }

        public class GetMany
        {
            private readonly ProductBacklogController controller;

            public GetMany()
            {
                var context = new Mock<IProductBacklogContext>();
                context.SetupGet(c => c.ProductBacklogs)
                    .Returns(DbSetUtils.GetMockDbSet(
                        new ProductBacklog { Id = 1 },
                        new ProductBacklog { Id = 2 }));
                this.controller = new ProductBacklogController(context.Object);
            }

            [Fact]
            public void ReturnsOk()
            {
                // act
                var result = this.controller.GetMany();

                // assert
                Assert.Equal(200, ((OkObjectResult)result).StatusCode);
            }

            [Fact]
            public void ReturnsAllProductBacklogsInDb()
            {
                // act
                var result = (OkObjectResult) this.controller.GetMany();

                // assert
                var backlogs = (List<ProductBacklog>)result.Value;
                Assert.Equal(2, backlogs.Count);
            }
        }
    }
}
