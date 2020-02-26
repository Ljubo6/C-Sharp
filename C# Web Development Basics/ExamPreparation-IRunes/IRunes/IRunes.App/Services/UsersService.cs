using IRunes.App.Data;
using IRunes.App.Models;
using IRunes.App.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace IRunes.App.Services
{
    public class UsersService : IUsersService
    {
        private readonly RunesDbContext db;

        public UsersService(RunesDbContext db)
        {
            this.db = db;
        }

        public string GetUserId(string username, string password)
        {
            var hashedPassword = this.Hash(password);
            var user = this.db.Users.FirstOrDefault(x => x.Username == username && x.Password == hashedPassword);
            if (user == null)
            {
                return null;
            }
            return user.Id;
        }

        public string GetUsername(string id)
        {
            var username = this.db.Users
                .Where(x => x.Id == id)
                .Select(x => x.Username)
                .FirstOrDefault();
            return username;
        }

        public void Register(RegisterInputViewModel input)
        {
            var user = new User
            {
                Username = input.Username,
                Password = this.Hash(input.Password),
                Email = input.Email
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();
        }

        private string Hash(string input)
        {
            if (input == null)
            {
                return null;
            }

            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }
    }
}
