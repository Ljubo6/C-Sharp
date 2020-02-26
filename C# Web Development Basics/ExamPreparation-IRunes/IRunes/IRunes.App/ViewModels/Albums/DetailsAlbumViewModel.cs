using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.App.ViewModels.Albums
{
    public class DetailsAlbumViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Cover { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<TracksInfoViewModel> Tracks { get; set; }
    }
}
