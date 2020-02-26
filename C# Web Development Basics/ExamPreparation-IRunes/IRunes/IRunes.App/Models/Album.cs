using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IRunes.App.Models
{
    public class Album
    {
        public Album()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Tracks = new HashSet<Track>();
        }
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public string Cover { get; set; }

        [Required]
        public decimal Price { get; set; }
        public ICollection<Track> Tracks { get; set; }

        //•	Id – a string (GuID), Primary key
        //•	Name – a string with min length 4 and max length 20 (inclusive) (required)
        //•	Cover – a string (a link to an image) (required)
        //•	Price – a decimal (sum of all Tracks’ prices, reduced by 13%) (required)
        //•	Tracks – a collection of Tracks

    }
}
