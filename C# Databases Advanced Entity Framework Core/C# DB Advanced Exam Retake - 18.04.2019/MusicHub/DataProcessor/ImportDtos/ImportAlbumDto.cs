using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicHub.DataProcessor.ImportDtos
{
    public class ImportAlbumDto
    {
        [StringLength(40, MinimumLength = 3), Required]
        public string Name { get; set; }

        [Required]
        public string ReleaseDate { get; set; }
    }
}
