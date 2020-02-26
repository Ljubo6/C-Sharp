using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.App.ViewModels.Users
{
    public class RegisterInputViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
    }
}
