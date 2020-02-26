namespace IRunes.App
{
    using System.Collections.Generic;
    using IRunes.App.Data;
    using IRunes.App.Services;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class Startup : IMvcApplication
    {
        public void Configure(IList<Route> serverRoutingTable)
        {
            using (var db = new RunesDbContext())
            {
                db.Database.EnsureCreated();
            }
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService,UsersService>();
            serviceCollection.Add<IAlbumsService,AlbumsService>();
            serviceCollection.Add<ITracksService,TracksService>();
        }
    }
}
