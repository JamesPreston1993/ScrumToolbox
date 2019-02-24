using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScrumToolbox.ProductBacklogs;
using ScrumToolbox.ProductBacklogs.Backlogs;
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
                context.SetupGet(c => c.ProductBacklogs).Returns(GetMockDbSet(new ProductBacklog()));
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
                context.SetupGet(c => c.ProductBacklogs).Returns(GetMockDbSet(new ProductBacklog()));
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

            private static DbSet<T> GetMockDbSet<T>(params T[] sourceList) where T : class
            {
                var queryable = sourceList.AsQueryable();

                var dbSet = new Mock<DbSet<T>>();
                dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
                dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
                dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
                dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

                return dbSet.Object;
            }
        }
    }
}
