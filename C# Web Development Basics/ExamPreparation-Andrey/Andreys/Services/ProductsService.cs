using Andreys.Data;
using Andreys.Models;
using Andreys.ViewModels.Home;
using Andreys.ViewModels.Products;
using SIS.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andreys.Services
{
    public class ProductsService : IProductsService
    {
        private readonly AndreysDbContext db;

        public ProductsService(AndreysDbContext db)
        {
            this.db = db;
        }

        public int AddProduct(AddProductViewModel input)
        {
            var categoryAsEnum = Enum.Parse<Category>(input.Category);
            var genderAsEnum = Enum.Parse<Gender>(input.Gender);
            var product = new Product
            {
                Name = input.Name,
                Description = input.Description,
                ImageUrl = input.ImageUrl,
                Category = categoryAsEnum,
                Gender = genderAsEnum,
                Price = input.Price
               
            };
            this.db.Products.Add(product);
            this.db.SaveChanges();
            return product.Id;
        }

        public void DeleteById(int id)
        {
            var product = this.db.Products.FirstOrDefault(x => x.Id == id);
            this.db.Remove(product);
            this.db.SaveChanges();
        }

        public IEnumerable<AllProductsViewModel> GetAllProducts()
        {
            return this.db.Products.Select(x => new AllProductsViewModel
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Name = x.Name,
                Price = x.Price,
            })
            .ToArray();
        }

        public Product GetProductById(int id)
        {
            return this.db.Products.FirstOrDefault(x => x.Id == id); ;
        }


    }
}
