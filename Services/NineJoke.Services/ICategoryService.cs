namespace NineJoke.Services
{
    using NineJoke.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface ICategoryService
    {
        void CreateCategory(string name);

        IQueryable<Category> GetAll();

        Category GetCategoryByName(string name);
    }
}
