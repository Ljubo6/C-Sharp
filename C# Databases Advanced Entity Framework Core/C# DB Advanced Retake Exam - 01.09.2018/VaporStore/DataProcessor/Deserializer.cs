namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.Data.Models.Enum;
    using VaporStore.DataProcessor.Dtos.Import;

    public static class Deserializer
	{
        public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{
            var gamesDto = JsonConvert.DeserializeObject<ImportGameDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            var games = new List<Game>();

            foreach (var gameDto in gamesDto)
            {
                var validDto = IsValid(gameDto);

                if (!validDto || gameDto.Tags.Count == 0)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var game = new Game
                {
                    Name = gameDto.Name,
                    Price = gameDto.Price,
                    ReleaseDate = DateTime.ParseExact(gameDto.ReleaseDate, "yyyy-MM-dd",CultureInfo.InvariantCulture),

                };

                Developer developer = GetDeveloper(context,gameDto.Developer);
                var genre = GetGenre(context,gameDto.Genre);

                game.Developer = developer;
                game.Genre = genre;

                foreach (var currentTag in gameDto.Tags)
                {
                    Tag tag = GetTag(context,currentTag);
                    game.GameTags.Add(new GameTag 
                    {
                        Game = game,
                        Tag = tag
                    });
                }

                games.Add(game);
                sb.AppendLine($"Added {game.Name} ({game.Genre.Name}) with {game.GameTags.Count} tags");
            }

            context.Games.AddRange(games);
            context.SaveChanges();
            return sb.ToString().TrimEnd();

        }

      
        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
		{
            var usersDto = JsonConvert.DeserializeObject<ImportUserDto[]>(jsonString);
            StringBuilder sb = new StringBuilder();

            var users = new List<User>();
            foreach (var userDto in usersDto)
            {

                if (!IsValid(userDto) || !userDto.Cards.All(IsValid))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                //bool isValidEnum = true;
                //foreach (var cardDto in userDto.Cards)
                //{
                //    var cardType = Enum.TryParse<CardType>(cardDto.Type, out CardType result);
                //    if (!cardType)
                //    {
                //        isValidEnum = false;
                //        break;
                //    }
                //}

                //if (!isValidEnum)
                //{
                //    sb.AppendLine("Invalid Data");
                //    continue;
                //}


                var user = new User 
                {
                    FullName = userDto.FullName,
                    Username = userDto.Username,
                    Email = userDto.Email,
                    Age = userDto.Age
                };

                foreach (var cardDto in userDto.Cards)
                {

                    user.Cards.Add(new Card
                    {
                        Number = cardDto.Number,
                        Cvc = cardDto.CVC,
                        Type = Enum.Parse<CardType>(cardDto.Type)
                    });
                }
                users.Add(user);
                sb.AppendLine($"Imported {user.Username} with {user.Cards.Count} cards");
            }

            context.Users.AddRange(users);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
		}

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{
            var xmlSerializer = new XmlSerializer(typeof(ImportPurchaseDto[]), new XmlRootAttribute("Purchases"));
            var purchasesDto = (ImportPurchaseDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();

            List<Purchase> purchases = new List<Purchase>();

            foreach (var purchaseDto in purchasesDto)
            {
                if (!IsValid(purchaseDto))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var isValidEnum = Enum.TryParse<PurchaseType>(purchaseDto.Type,out PurchaseType purchaseType);

                if (!isValidEnum)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var game = context.Games.FirstOrDefault(x => x.Name == purchaseDto.Title);
                var card = context.Cards.FirstOrDefault(x => x.Number == purchaseDto.Card);

                if (game == null || card == null)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var purchase = new Purchase
                {
                    Type = purchaseType,
                    Date = DateTime.ParseExact(purchaseDto.Date, "dd/MM/yyyy HH:mm",CultureInfo.InvariantCulture),
                    ProductKey = purchaseDto.Key,
                    Game = game,
                    Card = card                   
                };

                purchases.Add(purchase);
                sb.AppendLine($"Imported {purchase.Game.Name} for {purchase.Card.User.Username}");
            }

            context.Purchases.AddRange(purchases);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static Tag GetTag(VaporStoreDbContext context, string gameDtoCurrentTag)
        {
            var tag = context.Tags.FirstOrDefault(x => x.Name == gameDtoCurrentTag);
            if (tag == null)
            {
                tag = new Tag
                {
                    Name = gameDtoCurrentTag
                };
                context.Tags.Add(tag);
                context.SaveChanges();
            }
            return tag;
        }

        private static Genre GetGenre(VaporStoreDbContext context, string gameDtoGenre)
        {
            var genre = context.Genres.FirstOrDefault(x => x.Name == gameDtoGenre);
            if (genre == null)
            {
                genre = new Genre
                {
                    Name = gameDtoGenre
                };
                context.Genres.Add(genre);
                context.SaveChanges();
            }
            return genre;
        }

        private static Developer GetDeveloper(VaporStoreDbContext context, string gameDtoDeveloper)
        {
            var developer = context.Developers.FirstOrDefault(x => x.Name == gameDtoDeveloper);

            if (developer == null)
            {
                developer = new Developer
                {
                    Name = gameDtoDeveloper
                };

                context.Developers.Add(developer);
                context.SaveChanges();
            }
            return developer;

        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();
            return Validator.TryValidateObject(dto,validationContext,validationResult,true);
        }

    }
}