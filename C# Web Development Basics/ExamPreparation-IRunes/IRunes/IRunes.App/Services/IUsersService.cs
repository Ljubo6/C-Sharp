using IRunes.App.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.App.Services
{
    public interface IUsersService
    {
        void Register(RegisterInputViewModel input);
        string GetUserId(string username,string password);
        string GetUsername(string id);
    }
}
