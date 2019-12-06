namespace MusicHub.DataProcessor
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
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.DataProcessor.ImportDtos;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data";

        private const string SuccessfullyImportedWriter 
            = "Imported {0}";
        private const string SuccessfullyImportedProducerWithPhone 
            = "Imported {0} with phone: {1} produces {2} albums";
        private const string SuccessfullyImportedProducerWithNoPhone
            = "Imported {0} with no phone number produces {1} albums";
        private const string SuccessfullyImportedSong 
            = "Imported {0} ({1} genre) with duration {2}";
        private const string SuccessfullyImportedPerformer
            = "Imported {0} ({1} songs)";

        public static string ImportWriters(MusicHubDbContext context, string jsonString)
        {
            var writersDto = JsonConvert.DeserializeObject<ImportWriterDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            var writers = new List<Writer>();

            foreach (var writerDto in writersDto)
            {
                if (!IsValid(writerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var writer = new Writer
                {
                    Name = writerDto.Name,
                    Pseudonym = writerDto.Pseudonym
                };

                writers.Add(writer);
                sb.AppendLine(string.Format(SuccessfullyImportedWriter,writer.Name));
            }



            context.Writers.AddRange(writers);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportProducersAlbums(MusicHubDbContext context, string jsonString)
        {
            var producersDto = JsonConvert.DeserializeObject<ImportProdicerAndAlbumDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            var producers = new List<Producer>();

            foreach (var producerDto in producersDto)
            {
                if (!IsValid(producerDto) || !producerDto.Albums.All(IsValid))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var producer = new Producer
                {
                    Name = producerDto.Name,
                    Pseudonym = producerDto.Pseudonym,
                    PhoneNumber = producerDto.PhoneNumber,
                    Albums = producerDto.Albums.Select(a => new Album 
                    {
                        Name = a.Name,
                        ReleaseDate = DateTime
                                .ParseExact(a.ReleaseDate, @"dd/MM/yyyy", CultureInfo.InvariantCulture)
                    })
                    .ToArray()
                };

                producers.Add(producer);

                if (producer.PhoneNumber == null)
                {
                    sb.AppendLine(string.Format(SuccessfullyImportedProducerWithNoPhone,producer.Name,producer.Albums.Count));
                }
                else
                {
                    sb.AppendLine
                        (string.Format(SuccessfullyImportedProducerWithPhone
                        ,producer.Name,producer.PhoneNumber,producer.Albums.Count));
                }
            }



            context.Producers.AddRange(producers);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportSongs(MusicHubDbContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportSongXmlDto[]), new XmlRootAttribute("Songs"));
            var songsDto = (ImportSongXmlDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();

            var songs = new List<Song>();


            foreach (var songDto in songsDto)
            {
                bool isValidGenre = Enum.TryParse(songDto.Genre, out Genre genre);
                var isValidAlbum = context.Albums.Any(a => a.Id == songDto.AlbumId);
                var isValidWriter = context.Writers.Any(w => w.Id == songDto.WriterId);

                if (!IsValid(songDto) || !isValidGenre || !isValidAlbum || !isValidWriter)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var song = new Song
                {
                    Name = songDto.Name,
                    Duration = TimeSpan.ParseExact(songDto.Duration,"c",CultureInfo.InvariantCulture),
                    CreatedOn = DateTime.ParseExact(songDto.CreatedOn, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Genre = genre,
                    AlbumId = songDto.AlbumId,
                    WriterId = songDto.WriterId,
                    Price = songDto.Price

                };

                songs.Add(song);
                sb.AppendLine(string.Format(SuccessfullyImportedSong,song.Name,song.Genre,song.Duration));
            }


            context.Songs.AddRange(songs);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportSongPerformers(MusicHubDbContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportPerformerXmlDto[]), new XmlRootAttribute("Performers"));
            var performersDto = (ImportPerformerXmlDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();

            var performers = new List<Performer>();

            foreach (var performerDto in performersDto)
            {

                var isValidSongId = true;

                foreach (var song in performerDto.PerformersSongs)
                {
                    if (!context.Songs.Any(s => s.Id == song.Id))
                    {
                        isValidSongId = false;
                        break;
                    }
                }
                if (!IsValid(performerDto) || !isValidSongId)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var performer = new Performer
                {
                    FirstName = performerDto.FirstName,
                    LastName = performerDto.LastName,
                    Age = performerDto.Age,
                    NetWorth = performerDto.NetWorth,
                    PerformerSongs = performerDto.PerformersSongs
                    .Select(p => new SongPerformer
                    {
                        SongId = p.Id
                    })
                    .ToArray()
                };

                performers.Add(performer);

                sb.AppendLine(string.Format(SuccessfullyImportedPerformer,performer.FirstName,performer.PerformerSongs.Count));
            }

            context.Performers.AddRange(performers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }
        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();
            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}