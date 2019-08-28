namespace NineJoke.Services
{
    using System;
    using System.Linq;

    using NineJoke.Data;
    using NineJoke.Data.Models;

    public class CategoryService : ICategoryService
    {
        private const string Uncategorized = "Uncategorized";

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

        public void DeleteCategory(string id)
        {
            var category = this.GetCategoryById(id);
            var noCategory = this.GetCategoryByName(Uncategorized);

            var posts = this.context.Posts.Where(x => x.Categoryid == id).ToList();

            for (int i = 0; i < posts.Count; i++)
            {
                posts[i].Categoryid = noCategory.Id;
                noCategory.Popularity++;
            }

            this.context.Categories.Remove(category);
            this.context.SaveChanges();
        }

        public void EditCategory(string name, string id)
        {
            var category = this.GetCategoryById(id);
            category.Name = name;
            this.context.SaveChanges();
        }

        public IQueryable<Category> GetAll()
        {
            return this.context.Categories;
        }

        public Category GetCategoryById(string id)
        {
            var category = this.context.Categories.FirstOrDefault(x => x.Id == id);

            return category;
        }

        public Category GetCategoryByName(string name)
        {
            var category = this.context.Categories.FirstOrDefault(x => x.Name == name);

            return category;
        }
    }
}
