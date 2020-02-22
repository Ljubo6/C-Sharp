using Andreys.Models;
using Andreys.ViewModels.Home;
using Andreys.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.Services
{
    public interface IProductsService
    {
        IEnumerable<AllProductsViewModel> GetAllProducts();
        int AddProduct(AddProductViewModel input);
        Product GetProductById(int id);
        void DeleteById(int id);
    }
}
