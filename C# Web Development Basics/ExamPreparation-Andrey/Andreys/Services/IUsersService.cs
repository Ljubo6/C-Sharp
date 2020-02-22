using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.Services
{
    public interface IUsersService
    {
        void Register(string username, string email, string password);
        string GetUserId(string username, string password);
        bool EmailExists(string email);
        bool UsernameExists(string username);
    }
}
