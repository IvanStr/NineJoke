namespace NineJoke.Services
{
    using System;
    using System.Linq;

    using NineJoke.Data;
    using NineJoke.Data.Models;

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

            this.context.Add(category);
            this.context.SaveChanges();
        }

        public IQueryable<Category> GetAll()
        {
            return this.context.Categories;
        }

        public Category GetCategoryByName(string name)
        {
            var category = this.context.Categories.FirstOrDefault(x => x.Name == name);

            return category;
        }
    }
}
