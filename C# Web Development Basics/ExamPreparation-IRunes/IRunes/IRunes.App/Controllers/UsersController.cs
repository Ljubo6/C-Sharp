using IRunes.App.Services;
using IRunes.App.ViewModels.Users;
using SIS.HTTP;
using SIS.MvcFramework;

namespace IRunes.App.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        public HttpResponse Login()
        {
            return this.View();
        }
        [HttpPost]
        public HttpResponse Login(LoginInputViewModel input)
        {
            var userId = this.usersService.GetUserId(input.Username,input.Password);
            if (userId != null)
            {
                this.SignIn(userId);
                
                return this.Redirect("/");
            }
            return this.View();
        }

        public HttpResponse Register()
        {
            return this.View();
        }
        [HttpPost]
        public HttpResponse Register(RegisterInputViewModel input)
        {
            this.usersService.Register(input);
            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            if (!IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            this.SignOut();
            return this.Redirect("/");
        }
    }
}
