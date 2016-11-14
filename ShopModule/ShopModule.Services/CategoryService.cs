using ShopModule.Core.Entities;
using ShopModule.Core.Interfaces.Repositories.Base;
using ShopModule.Data;
using ShopModule.Data.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace ShopModule.Services
{
    public class CategoryService
    {
        //Category repository
        private IRepository<Category> categoryRepository;
        //Context for db
        private DataContext context;

        public CategoryService()
        {
            this.context = new DataContext();
            this.categoryRepository = new Repository<Category>(this.context);
        }

        //Get all categories from database
        public List<Category> GetAll()
        {
            return this.categoryRepository.GetAll().ToList<Category>();
        }

        //Add new category
        public bool AddCategory(string categoryName)
        {
            if(categoryName != null)
            {
                Category category = new Category()
                {
                    Name = categoryName
                };

                this.categoryRepository.Add(category);
                this.categoryRepository.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
