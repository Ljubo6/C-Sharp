using IRunes.App.Data;
using IRunes.App.Models;
using IRunes.App.ViewModels.Albums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRunes.App.Services
{
    public class AlbumsService : IAlbumsService
    {
        private readonly RunesDbContext db;

        public AlbumsService(RunesDbContext db)
        {
            this.db = db;
        }

        public void CreateAlbum(CreateAlbumViewModel input)
        {
            var album = new Album
            {
                Name = input.Name,
                Cover = input.Cover,
                Price = 0.0m
            };

            this.db.Albums.Add(album);
            this.db.SaveChanges();
        }

        public IEnumerable<Album> GetAllAlbums()
        {
            var albums = this.db.Albums.Select(x => new Album
            {
                Id = x.Id,
                Name = x.Name,
                Cover = x.Cover,
                Price = x.Price
            }).ToArray();
            return albums;
        }

        public DetailsAlbumViewModel GetDetails(string id)
        {
            var album = this.db.Albums.Where(x => x.Id == id)
                .Select(x => new DetailsAlbumViewModel
                {

                    Id = x.Id,
                    Name = x.Name,
                    Cover = x.Cover,
                    Price = x.Price,
                    Tracks = x.Tracks.Select(t => new TracksInfoViewModel
                    {
                        Id = t.Id,
                        Name = t.Name
                    })

                }).FirstOrDefault();
            return album;
        }
    }
}
