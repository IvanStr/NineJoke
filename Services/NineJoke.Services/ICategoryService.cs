using NineJoke.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NineJoke.Services
{
    public interface ICategoryService
    {
        void CreateCategory(string name);

        IQueryable<Category> GetAll();
    }
}
