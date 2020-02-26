using IRunes.App.Services;
using IRunes.App.ViewModels.Albums;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.App.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumsService albumsService;

        public AlbumsController(IAlbumsService albumsService)
        {
            this.albumsService = albumsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var albums = this.albumsService.GetAllAlbums();
            return this.View(albums);
        }

        public HttpResponse Create()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            return this.View();
        }
        [HttpPost]
        public HttpResponse Create(CreateAlbumViewModel input)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            if (input.Name.Length < 4 || input.Name.Length > 20)
            {
                return this.Error("Name should be with length between 4 and 20");
            }

            if (string.IsNullOrWhiteSpace(input.Cover))
            {
                return this.Error("Cover is required.");
            }

            this.albumsService.CreateAlbum(input);
            return this.Redirect($"/Albums/All");
        }
        public HttpResponse Details(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var viewModel = this.albumsService.GetDetails(id);
            return this.View(viewModel);
        }
    }
}
