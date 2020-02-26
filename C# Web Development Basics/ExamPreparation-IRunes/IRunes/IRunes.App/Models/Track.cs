using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IRunes.App.Models
{
    public class Track
    {
        public Track()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public string Link { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string AlbumId { get; set; }
        public Album Album { get; set; }

        //•	Id – a string (GuID), Primary key
        //•	Name – a string with min length 4 and max length 20 (inclusive) (required)
        //•	Link – a string (a link to a video) (required)
        //•	Price – a decimal (required)

    }
}
