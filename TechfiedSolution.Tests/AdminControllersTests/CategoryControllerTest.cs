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
        Assert.Equal(3, model.Count); // Check that there are 2 categories in the model
    }


    [Fact]
    public async Task Details_IdIsNull_ReturnsNotFound()
    {
        // Arrange
        var dbContextMock = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new ApplicationDbContext(dbContextMock);
        var controller = new CategoryController(dbContext);

        // Act
        var result = await controller.Details(null);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Create_ValidModel_ReturnsRedirectToActionResult()
    {
        // Arrange
        var dbContextMock = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new ApplicationDbContext(dbContextMock);
        var controller = new CategoryController(dbContext);
        var category = new Category { Id = 3, Title = "Test Category 3", ThumbnailImagePath = "default.jpg" };
        // Act
        var result = await controller.Create(category);

        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }

    [Fact]
    public async Task DeleteConfirmed_ValidId_ReturnsRedirectToActionResult()
    {
        // Arrange
        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new ApplicationDbContext(dbContextOptions);
        dbContext.Category.Add(new Category
        {
            Id = 1,
            Title = "Test Title",
            ThumbnailImagePath = "Test.jpg"
        }); // a sample category with ID 1 to the in-memory database
        dbContext.SaveChanges();

        var controller = new CategoryController(dbContext);

        // Act
        var result = await controller.DeleteConfirmed(1);

        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }


}
