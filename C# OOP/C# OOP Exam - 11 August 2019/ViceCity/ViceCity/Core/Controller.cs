using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViceCity.Core.Contracts;
using ViceCity.Models.Guns;
using ViceCity.Models.Guns.Contracts;
using ViceCity.Models.Neghbourhoods;
using ViceCity.Models.Neghbourhoods.Contracts;
using ViceCity.Models.Players;
using ViceCity.Models.Players.Contracts;
using ViceCity.Repositories;
using ViceCity.Repositories.Contracts;

namespace ViceCity.Core
{
    class Controller : IController
    {
        private readonly Queue<IGun> guns;
        private IList<IPlayer> civilPlayers;
        private IPlayer mainPlayer;
        private INeighbourhood gangNeihbourhood;
        private GunRepository gunRepository;
        public Controller()
        {
            this.guns = new Queue<IGun>();
            this.civilPlayers = new List<IPlayer>();
            this.mainPlayer = new MainPlayer();
            this.gangNeihbourhood = new GangNeighbourhood();
            this.gunRepository = new GunRepository();
        }
        public string AddGun(string type, string name)
        {
            IGun gun = null;
            if (type == "Pistol")
            {
                gun = new Pistol(name);
            }
            else if (type == "Rifle")
            {
                gun = new Rifle(name);
            }
            else
            {
                return "Invalid gun type!";
            }
            this.guns.Enqueue(gun);
            return $"Successfully added {name} of type: {type}";
        }

        public string AddGunToPlayer(string name)
        {
            if (this.guns.Count == 0)
            {
                return "There are no guns in the queue!";
            }
            if (name == "Vercetti")
            {
                var gunToAdd = this.guns.Dequeue();
                this.mainPlayer.GunRepository.Add(gunToAdd);
                return $"Successfully added {gunToAdd.Name} to the Main Player: Tommy Vercetti";
            }
            if (!this.civilPlayers.Any(x => x.Name == name))
            {
                return "Civil player with that name doesn't exists!";
            }
            var gunAdd = this.guns.Dequeue();
            var playerToAddGun = this.civilPlayers.FirstOrDefault(x => x.Name == name);
            playerToAddGun.GunRepository.Add(gunAdd);
            return $"Successfully added {gunAdd.Name} to the Civil Player: {playerToAddGun.Name}";
        }

        public string AddPlayer(string name)
        {
            IPlayer civilPlayer = new CivilPlayer(name);
            this.civilPlayers.Add(civilPlayer);
            return $"Successfully added civil player: {civilPlayer.Name}!";
        }

        public string Fight()
        {
            var mainPlayerPointsAtBegining = mainPlayer.LifePoints;
            var civilplayerPointsAtBegining = civilPlayers.Sum(x => x.LifePoints);
            this.gangNeihbourhood.Action(this.mainPlayer, this.civilPlayers);

            var mainPlayerPointsAtTheEnd = mainPlayer.LifePoints;
            var civilplayerPointsAtTheEnd = civilPlayers.Sum(x => x.LifePoints);

            if (mainPlayerPointsAtBegining == mainPlayerPointsAtTheEnd
                && civilplayerPointsAtBegining == civilplayerPointsAtTheEnd)
            {
                return "Everything is okay!";
            }

            var mainPlayerLifePoints = this.mainPlayer.LifePoints;
            var deadCivilPlayers = this.civilPlayers.Where(x => x.IsAlive == false).ToList().Count();
            var civilPlayersCount = this.civilPlayers.Where(x => x.IsAlive == true).ToList().Count();

            StringBuilder sb = new StringBuilder();
            sb.
            AppendLine($"A fight happened:").
            AppendLine($"Tommy live points: {mainPlayerLifePoints}!").
            AppendLine($"Tommy has killed: {deadCivilPlayers} players!").
            AppendLine($"Left Civil Players: {civilPlayersCount}!");
            return sb.ToString().TrimEnd();

        }
    }
}
