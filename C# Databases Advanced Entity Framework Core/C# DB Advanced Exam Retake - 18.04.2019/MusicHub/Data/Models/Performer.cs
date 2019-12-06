using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicHub.Data.Models
{
    public class Performer
    {
        public Performer()
        {
            this.PerformerSongs = new HashSet<SongPerformer>();
        }
        public int Id { get; set; }

        [StringLength(20,MinimumLength = 3),Required]
        public string FirstName { get; set; }

        [StringLength(20, MinimumLength = 3), Required]
        public string LastName { get; set; }

        [Range(18,70),Required]
        public int Age { get; set; }

        [Range(typeof(decimal), "0.0", "79228162514264337593543950335"), Required]
        public decimal NetWorth { get; set; }
        public ICollection<SongPerformer> PerformerSongs { get; set; }
    }
}
