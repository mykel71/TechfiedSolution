using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechfiedSolution.Areas.Admin.Controllers;
using TechfiedSolution.Data;
using TechfiedSolution.Entities;

namespace TechfiedSolution.Tests.AdminControllersTests;

public class CategoryItemControllerTest
{
    [Fact]
    public async Task Index_ReturnsViewResult_WithListOfCategoryItems()
    {
        // Arrange
        var categoryId = 1;
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new ApplicationDbContext(options);
        dbContext.CategoryItem.Add(new CategoryItem { Id = 1, CategoryId = categoryId, Title = "Item 1" });
        dbContext.CategoryItem.Add(new CategoryItem { Id = 2, CategoryId = categoryId, Title = "Item 2" });
        dbContext.SaveChanges();

        var controller = new CategoryItemController(dbContext);

        // Act
        var result = await controller.Index(categoryId);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = viewResult.Model as List<CategoryItem>;
        Assert.NotNull(model); // Check that the model is not null
        Assert.Equal(2, model.Count); // Check that there are 2 category items in the model
    }


        [Fact]
    public async Task Details_IdIsNull_ReturnsNotFound()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new ApplicationDbContext(options);
        var controller = new CategoryItemController(dbContext);

        // Act
        var result = await controller.Details(null);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Create_ValidModel_ReturnsRedirectToActionResult()
    {
        // Arrange
        var categoryId = 1;
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new ApplicationDbContext(options);
        var controller = new CategoryItemController(dbContext);
        var categoryItem = new CategoryItem { Id = 3, CategoryId = categoryId, Title = "Test Item 3" };

        // Act
        var result = await controller.Create(categoryItem);

        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }

}
