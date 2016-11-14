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
        private IRepository<Category> categoryRepository;
        private DataContext context;

        public CategoryService()
        {
            this.context = new DataContext();
            this.categoryRepository = new Repository<Category>(this.context);
        }

        public List<Category> GetAll()
        {
            return this.categoryRepository.GetAll().ToList<Category>();
        }

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
