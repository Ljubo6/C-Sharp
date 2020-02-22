namespace Andreys.App.Controllers
{
    using Andreys.Services;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class HomeController : Controller
    {
        private readonly IProductsService productService;

        public HomeController(IProductsService productService)
        {
            this.productService = productService;
        }
        [HttpGet("/")]
        public HttpResponse IndexSlash()
        {
            return this.Index();
        }
        public HttpResponse Index()
        {
            if (IsUserLoggedIn())
            {
                var products = this.productService.GetAllProducts();
                return this.View(products,"Home");
            }
            return this.View();
        }
    }
}
