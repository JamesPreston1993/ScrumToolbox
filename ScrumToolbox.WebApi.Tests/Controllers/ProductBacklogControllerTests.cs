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

        public class Update
        {
            private readonly ProductBacklogController controller;

            public Update()
            {
                var context = new Mock<IProductBacklogContext>();
                context.SetupGet(c => c.ProductBacklogs)
                    .Returns(DbSetUtils.GetMockDbSet(new ProductBacklog { Id = 1, Name = "Original Value" }));
                this.controller = new ProductBacklogController(context.Object);
            }

            [Fact]
            public void ReturnsOk()
            {
                // arrange
                var dto = new ProductBacklogDto();

                // act
                var result = this.controller.Update(1, dto);

                // assert
                Assert.Equal(200, ((OkObjectResult)result).StatusCode);
            }


            [Fact]
            public void SetsNameWhenProvided()
            {
                // arrange
                var dto = new ProductBacklogDto
                {
                    Name = "New value"
                };

                // act
                var result = (OkObjectResult) this.controller.Update(1, dto);

                // assert
                var backlog = (ProductBacklog)result.Value;
                Assert.Equal("New value", backlog.Name);
            }

            [Fact]
            public void DoesNotSetNameWhenNotProvided()
            {
                // arrange
                var dto = new ProductBacklogDto();

                // act
                var result = (OkObjectResult)this.controller.Update(1, dto);

                // assert
                var backlog = (ProductBacklog)result.Value;
                Assert.Equal("Original Value", backlog.Name);
            }

            [Fact]
            public void ReturnsBadRequestWhenIdIsInvalid()
            {
                // arrange
                var dto = new ProductBacklogDto();

                // act
                var result = this.controller.Update(2, dto);

                // assert
                Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);
            }
        }

        public class Delete
        {
            private readonly ProductBacklogController controller;

            public Delete()
            {
                var context = new Mock<IProductBacklogContext>();
                context.SetupGet(c => c.ProductBacklogs)
                    .Returns(DbSetUtils.GetMockDbSet(new ProductBacklog { Id = 1 }));
                this.controller = new ProductBacklogController(context.Object);
            }

            [Fact]
            public void ReturnsNoContent()
            {
                // act
                var result = this.controller.Delete(1);

                // assert
                Assert.Equal(204, ((NoContentResult)result).StatusCode);
            }

            [Fact]
            public void ReturnsBadRequestWhenIdIsInvalid()
            {
                // act
                var result = this.controller.Delete(2);

                // assert
                Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);
            }
        }
    }
}
