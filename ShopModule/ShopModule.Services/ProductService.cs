using ShopModule.Core.Entities;
using ShopModule.Core.Interfaces.Repositories.Base;
using ShopModule.Data;
using ShopModule.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopModule.Services
{
    public class ProductService
    {
        //Product repository
        private IRepository<Product> productRepository;
        //Context for db
        private DataContext context;

        public ProductService()
        {
            this.context = new DataContext();
            this.productRepository = new Repository<Product>(this.context);
        }

        //Get all Product from db
        public List<Product> GetAll()
        {
            return this.productRepository.GetAll().ToList<Product>();
        }

        //Add new Product
        public bool AddProduct(string name, decimal price, string description, string categoryId, int pictureId)
        {
            if(Convert.ToInt32(categoryId) != 0 && pictureId != 0)
            {
                Product product = new Product()
                {
                    Name = name,
                    Price = price,
                    Description = description,
                    CategoryId = Convert.ToInt32(categoryId),
                    PictureId = pictureId
                };

                this.productRepository.Add(product);
                this.productRepository.SaveChanges();

                return true;
            }

            return false;
        }

        //Get product from db by id
        public Product GetById(int id)
        {
            return this.productRepository.GetById(id);
        }

        //Edit product
        public bool EditProduct(int id, string name, decimal price, string description, string categoryId, int pictureId)
        {
            if (id != 0 && pictureId != 0)
            {
                var product = this.productRepository.GetById(id);
                product.Name = name;
                product.Price = price;
                product.Description = description;
                product.CategoryId = Convert.ToInt32(categoryId);
                product.PictureId = pictureId;

                this.productRepository.SaveChanges();

                return true;
            }

            return false;
        }

        //Delete Product
        public void DeleteProduct(int id)
        {
            if(id!=0)
            {
                this.productRepository.Delete(this.productRepository.GetById(id));
                this.productRepository.SaveChanges();
            }
        }
    }
}
