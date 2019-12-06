namespace VaporStore.DataProcessor
{
	using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.DataProcessor.Dtos.Export;
    using Formatting = Newtonsoft.Json.Formatting;

    public static class Serializer
	{
		public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
		{
            var genres = context
                .Genres
                .Where(g => genreNames.Contains(g.Name))
                .Select(x => new
                {
                    Id = x.Id,
                    Genre = x.Name,
                    Games = x.Games
                    .Where(p => p.Purchases.Any())
                    .Select(g => new
                    {
                        Id = g.Id,
                        Title = g.Name,
                        Developer = g.Developer.Name,
                        Tags = string.Join(", ", g.GameTags.Select(t => t.Tag.Name).ToArray()),
                        Players = g.Purchases.Count
                    })
                    .OrderByDescending(p => p.Players)
                    .ThenBy(g => g.Id)
                    .ToArray(),

                    TotalPlayers = x.Games.Sum(p => p.Purchases.Count)
                })
                .OrderByDescending(t => t.TotalPlayers)
                .ThenBy(g => g.Id)
                .ToArray();

            var json = JsonConvert.SerializeObject(genres,Formatting.Indented);

            return json;
		}

		public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
		{
            var users = context
                .Users
                .Select(u => new ExportUserDto
                {
                    Username = u.Username,
                    Purchases = u.Cards.SelectMany(p => p.Purchases)
                    .Where(p => p.Type.ToString() == storeType)
                    .Select(p => new ExportPurchaseDto
                    {
                        Card = p.Card.Number,
                        Cvc = p.Card.Cvc,
                        Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                        Game = new ExportGameDto
                        {
                            Title = p.Game.Name,
                            Genre = p.Game.Genre.Name,
                            Price = p.Game.Price
                        }
                    })
                    .OrderBy(p => p.Date)
                    .ToArray(),
                    TotalSpent = u.Cards.SelectMany(p => p.Purchases)
                    .Where(p => p.Type.ToString() == storeType)
                    .Sum(p => p.Game.Price)

                })
                .Where(p => p.Purchases.Any())
                .OrderByDescending(u => u.TotalSpent)
                .ThenBy(u => u.Username)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(ExportUserDto[]), new XmlRootAttribute("Users"));

            var sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(new StringWriter(sb), users, namespaces);

            return sb.ToString().TrimEnd();
        }
	}
}