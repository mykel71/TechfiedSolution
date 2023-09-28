using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using TechfiedSolution.Areas.Admin.Controllers;
using TechfiedSolution.Data;
using TechfiedSolution.Entities;

namespace TechfiedSolution.Tests.AdminControllersTests;

public class CategoryControllerTest
{
    [Fact]
    public async Task Index_ReturnsViewResult_WithListOfCategories()
    {
        // Arrange
        var categories = new List<Category>
        {
            new Category { Id = 1, Title = "Category 1", ThumbnailImagePath = "default.jpg" },
            new Category { Id = 2, Title = "Category 2", ThumbnailImagePath = "default.jpg" }
        };

        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new ApplicationDbContext(dbContextOptions);
        dbContext.AddRange(categories);
        dbContext.SaveChanges();

        var controller = new CategoryController(dbContext);

        // Act
        var result = await controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<List<Category>>(viewResult.Model);
        Assert.Equal(2, model.Count); // Check that there are 2 categories in the model
    }
}
