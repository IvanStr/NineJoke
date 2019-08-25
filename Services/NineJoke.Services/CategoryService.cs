using NineJoke.Data.Models;
using NineJoke.Data;
using System.Data;
using System;
using System.Collections.Generic;
using System.Text;
using NineJoke.Data.Common.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace NineJoke.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext context;

        public CategoryService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void CreateCategory(string name)
        {
            var category = new Category
            {
                Name = name,
                Popularity = 0,
                CreatedOn = DateTime.Now,
            };

            context.Add(category);
            context.SaveChanges();
        }

        public IQueryable<Category> GetAll()
        {
            return this.context.Categories;
        }
    }
}
