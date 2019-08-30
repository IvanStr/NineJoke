using Microsoft.EntityFrameworkCore;
using NineJoke.Data;
using NineJoke.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace NineJoke.Services.Tests
{
    public class CategoryServiceTests
    {
        [Fact]
        public void CreateCategoryShouldCreateCategory()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: "CreateCategoryShouldCreateCategoryDB")
                 .Options;

            var dbContext = new ApplicationDbContext(options);

            var categoryService = new CategoryService(dbContext);

            string name = "Funny";

            categoryService.CreateCategory(name);

            var categoies = dbContext.Categories.ToList();

            Assert.Single(categoies);
        }

        [Fact]
        public void DeleteCategoryShouldDeleteCategory()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: "DeleteCategoryShouldDeleteCategoryDB")
                 .Options;

            var dbContext = new ApplicationDbContext(options);

            var categoryService = new CategoryService(dbContext);

            var category = new Category
            {
                Name = "Funny",
                Popularity = 0,
                CreatedOn = DateTime.Now,
            };

            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            categoryService.DeleteCategory(category.Id);

            var categoies = dbContext.Categories.ToList();

            Assert.Empty(categoies);
        }

        [Fact]
        public void EditCategoryShouldRanameCategory()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: "EditCategoryShouldRanameCategoryDB")
                 .Options;

            var dbContext = new ApplicationDbContext(options);

            var categoryService = new CategoryService(dbContext);

            var category = new Category
            {
                Name = "Funny",
                Popularity = 0,
                CreatedOn = DateTime.Now,
            };

            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            string name = "NotFunny";

            categoryService.EditCategory(name, category.Id);

            Assert.Equal(name, category.Name);
        }

        [Fact]
        public void GetAllShouldReturnAllCategories()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: "GetAllShouldReturnAllCategoriesDB")
                 .Options;

            var dbContext = new ApplicationDbContext(options);

            var categoryService = new CategoryService(dbContext);

            var category = new Category
            {
                Name = "Funny",
                Popularity = 0,
                CreatedOn = DateTime.Now,
            };

            var category2 = new Category
            {
                Name = "ASd",
                Popularity = 0,
                CreatedOn = DateTime.Now,
            };

            dbContext.Categories.Add(category);
            dbContext.Categories.Add(category2);
            dbContext.SaveChanges();

            var categories = categoryService.GetAll().ToList();

            Assert.Equal(2, categories.Count);
        }

        [Fact]
        public void GetCategoryByIdReturnGetCategoryById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: "GetCategoryByIdReturnGetCategoryByIdDB")
                 .Options;

            var dbContext = new ApplicationDbContext(options);

            var categoryService = new CategoryService(dbContext);

            var category = new Category
            {
                Name = "Funny",
                Popularity = 0,
                CreatedOn = DateTime.Now,
            };

            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            var category2 = categoryService.GetCategoryById(category.Id);

            Assert.Equal(category2.Name, category.Name);
        }

        [Fact]
        public void GetCategoryByNameShouldReturnGetCategoryByName()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: "GetCategoryByNameShouldReturnGetCategoryByNameDB")
                 .Options;

            var dbContext = new ApplicationDbContext(options);

            var categoryService = new CategoryService(dbContext);

            var category = new Category
            {
                Name = "Funny",
                Popularity = 0,
                CreatedOn = DateTime.Now,
            };

            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            var category2 = categoryService.GetCategoryByName(category.Name);

            Assert.Equal(category2.Name, category.Name);
        }
    }
}
