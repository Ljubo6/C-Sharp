using System;
using System.ComponentModel.DataAnnotations;

namespace IRunes.App.Models
{
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }



        //•	Id – a string (GuID), Primary key
        //•	Username – a string with min length 4 and max length 10 (inclusive) (required)
        //•	Password – a string with min length 6 and max length 20 (inclusive)  - hashed in the database(required)
        //•	Email – a string (required)

    }
}
