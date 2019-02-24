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
            [Fact]
            public void ReturnsCreated()
            {
                // arrange
                var context = new Mock<IProductBacklogContext>();
                context.SetupGet(c => c.ProductBacklogs).Returns(DbSetUtils.GetMockDbSet(new ProductBacklog()));
                var controller = new ProductBacklogController(context.Object);

                // act
                var result = controller.Create(new ProductBacklogDto
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
                // arrange
                var context = new Mock<IProductBacklogContext>();
                context.SetupGet(c => c.ProductBacklogs).Returns(DbSetUtils.GetMockDbSet(new ProductBacklog()));
                var controller = new ProductBacklogController(context.Object);

                // act
                var result = controller.Create(new ProductBacklogDto
                {
                    Name = name
                });

                // assert
                Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);
                Assert.Equal("Name cannot be empty.", ((BadRequestObjectResult)result).Value);
            }
        }
    }
}
