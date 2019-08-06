using MXGP.Core.Contracts;
using MXGP.Core.Factories.Contracts;
using MXGP.Models.Motorcycles.Contracts;
using MXGP.Models.Races.Contracts;
using MXGP.Models.Riders.Contracts;
using MXGP.Repositories;
using MXGP.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MXGP.Core
{
    class ChampionshipController : IChampionshipController
    {
        private const int RequiredRidersCount = 3;
        private IMotorcycleFactory motorcycleFactory;
        private IRiderFactory riderFactory;
        private IRaceFactory raceFactory;

        private MotorcycleRepository motorcycleRepository;
        private RiderRepository riderRepository;
        private RaceRepository raceRepository;

        public ChampionshipController()
        {
            this.motorcycleFactory = new MotorcycleFactory() ;
            this.riderFactory = new RiderFactory();
            this.raceFactory = new RaceFactory();
            this.motorcycleRepository = new MotorcycleRepository();
            this.riderRepository = new RiderRepository();
            this.raceRepository = new RaceRepository();
        }

        public string AddMotorcycleToRider(string riderName, string motorcycleModel)
        {
            var rider = this.riderRepository.GetByName(riderName);

            if (rider == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        ExceptionMessages.RiderNotFound,
                        riderName));
            }

            var motorcycle = this.motorcycleRepository.GetByName(motorcycleModel);

            if (motorcycle == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        ExceptionMessages.MotorcycleNotFound,
                        motorcycleModel));
            }

            rider.AddMotorcycle(motorcycle);

            return string.Format(OutputMessages.MotorcycleAdded, riderName, motorcycleModel);
        }

        public string AddRiderToRace(string raceName, string riderName)
        {
            var race = this.raceRepository.GetByName(raceName);

            if (race == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        ExceptionMessages.RaceNotFound,
                        raceName));
            }

            var rider = this.riderRepository.GetByName(riderName);

            if (rider == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        ExceptionMessages.RiderNotFound,
                        riderName));
            }

            race.AddRider(rider);

            return string.Format(OutputMessages.RiderAdded, riderName, raceName);
        }

        public string CreateMotorcycle(string type, string model, int horsePower)
        {
            if (this.motorcycleRepository.GetAll().Any(x => x.Model == model))
            {
                throw new ArgumentException(
                    string.Format(
                        ExceptionMessages.MotorcycleExists,
                        model));
            }

            var motorcycle = this.motorcycleFactory.CreateMotorcycle(type, model, horsePower);
            this.motorcycleRepository.Add(motorcycle);

            return string.Format(
                OutputMessages.MotorcycleCreated,
                motorcycle.GetType().Name,
                motorcycle.Model);
        }

        public string CreateRace(string name, int laps)
        {
            if (this.raceRepository.GetAll().Any(x => x.Name == name))
            {
                throw new InvalidOperationException(
                    string.Format(
                        ExceptionMessages.RaceExists,
                        name));
            }

            var race = this.raceFactory.CreateRace(name, laps);
            this.raceRepository.Add(race);

            return string.Format(OutputMessages.RaceCreated, name);
        }

        public string CreateRider(string riderName)
        {
            if (this.riderRepository.GetAll().Any(x => x.Name == riderName))
            {
                throw new ArgumentException(
                    string.Format(
                        ExceptionMessages.RiderExists,
                        riderName));
            }

            var rider = this.riderFactory.CreateRider(riderName);
            this.riderRepository.Add(rider);

            return string.Format(OutputMessages.RiderCreated, riderName);
        }

        public string StartRace(string raceName)
        {
            var race = this.raceRepository
                .GetByName(raceName);

            if (race == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        ExceptionMessages.RaceNotFound,
                        raceName));
            }

            var riders = race
                .Riders
                .OrderByDescending(x => x.Motorcycle
                    .CalculateRacePoints(race.Laps))
                .ToList();

            if (riders.Count < RequiredRidersCount)
            {
                throw new InvalidOperationException(
                    string.Format(
                        ExceptionMessages.RaceInvalid,
                        raceName,
                        RequiredRidersCount));
            }

            riders[0].WinRace();

            var result = new StringBuilder();

            result.AppendLine($"Rider {riders[0].Name} wins {race.Name} race.");
            result.AppendLine($"Rider {riders[1].Name} is second in {race.Name} race.");
            result.AppendLine($"Rider {riders[2].Name} is third in {race.Name} race.");

            this.raceRepository.Remove(race);

            return result.ToString().TrimEnd();
        }
    }
}
