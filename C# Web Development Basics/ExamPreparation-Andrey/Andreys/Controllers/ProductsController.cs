using Andreys.Services;
using Andreys.ViewModels.Products;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }
        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            return this.View();
        }
        [HttpPost]
        public HttpResponse Add(AddProductViewModel input)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            if (input.Name.Length < 4 || input.Name.Length > 20)
            {
                return this.View();
            }
            if (string.IsNullOrEmpty(input.Description))
            {
                return this.View();
            }
            if (input.Description.Length > 10)
            {
                return this.View();
            }

            var productId = this.productsService.AddProduct(input);

            return this.Redirect($"/Products/Details?id={productId}");
        }
        public HttpResponse Details(int id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var product = this.productsService.GetProductById(id);
            return this.View(product);
        }
        public HttpResponse Delete(int id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.productsService.DeleteById(id);
            return this.Redirect("/");
        }
    }
}
