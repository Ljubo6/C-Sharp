using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporStore.Data.Models
{
    public class User
    {
        //•	Id – integer, Primary Key
        //•	Username – text with length[3, 20] (required)
        //•	FullName – text, which has two words, consisting of Latin letters.Both start with an upper letter and are separated by a single space(ex. "John Smith") (required)
        //•	Email – text(required)
        //•	Age – integer in the range[3, 103] (required)
        //•	Cards – collection of type Card
        public User()
        {
            this.Cards = new HashSet<Card>();
        }
        [Key]
        public int Id { get; set; }

        [MinLength(3),MaxLength(20),Required]
        public string Username { get; set; }

        [RegularExpression("^[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+$"),Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Range(3,103),Required]
        public int Age { get; set; }
        public ICollection<Card> Cards { get; set; }
    }
}
