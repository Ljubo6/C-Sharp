using IRunes.App.Services;
using IRunes.App.ViewModels.Home;
using SIS.HTTP;
using SIS.MvcFramework;

namespace IRunes.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersService usersService;

        public HomeController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        //[HttpGet("/")]
        //public HttpResponse IndexSlash()
        //{
        //    return this.Index();
        //}
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (IsUserLoggedIn())
            {
                var inputView = new InputViewModel();
                  inputView.Username =  this.usersService.GetUsername(this.User);
                return this.View(inputView,"Home");
            }
            return this.View();
        }
    }
}
