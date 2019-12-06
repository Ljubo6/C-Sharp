using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VaporStore.Data.Models.Enum;

namespace VaporStore.Data.Models
{
    public class Card
    {
        //•	Id – integer, Primary Key
        //•	Number – text, which consists of 4 pairs of 4 digits, separated by spaces(ex. “1234 5678 9012 3456”) (required)
        //•	Cvc – text, which consists of 3 digits(ex. “123”) (required)
        //•	Type – enumeration of type CardType, with possible values(“Debit”, “Credit”) (required)
        //•	UserId – integer, foreign key(required)
        //•	User – the card’s user(required)
        //•	Purchases – collection of type Purchase
        public Card()
        {
            this.Purchases = new HashSet<Purchase>();
        }
        [Key]
        public int Id { get; set; }

        [RegularExpression("^[A-Z0-9]{4} [A-Z0-9]{4} [A-Z0-9]{4} [A-Z0-9]{4}$"),Required]
        public string Number { get; set; }

        [RegularExpression("[0-9]{3}"),Required]
        public string Cvc { get; set; }

        [Required]
        public CardType Type { get; set; }

        [ForeignKey(nameof(User)),Required]
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Purchase> Purchases { get; set; }

    }
}
