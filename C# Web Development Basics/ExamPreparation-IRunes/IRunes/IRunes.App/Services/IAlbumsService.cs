using IRunes.App.Models;
using IRunes.App.ViewModels.Albums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.App.Services
{
    public interface IAlbumsService
    {
        IEnumerable<Album> GetAllAlbums();
        void CreateAlbum(CreateAlbumViewModel input);
        DetailsAlbumViewModel GetDetails(string id);
    }
}
