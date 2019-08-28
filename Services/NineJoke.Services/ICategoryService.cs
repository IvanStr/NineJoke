namespace NineJoke.Services
{
    using System.Linq;

    using NineJoke.Data.Models;

    public interface ICategoryService
    {
        void CreateCategory(string name);

        IQueryable<Category> GetAll();

        Category GetCategoryByName(string name);

        Category GetCategoryById(string id);

        void EditCategory(string name, string id);

        void DeleteCategory(string id);
    }
}
