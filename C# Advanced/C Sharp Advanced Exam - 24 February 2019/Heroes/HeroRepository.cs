using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes
{
    public class HeroRepository
    {
        private List<Hero> heroes;
        public HeroRepository()
        {
            this.heroes = new List<Hero>();
        }
        public int Count => heroes.Count;
        public void Add(Hero hero)
        {
            heroes.Add(hero);
        }
        public void Remove(string name)
        {
            var currentHero = heroes.FirstOrDefault(x => x.Name == name);
            heroes.Remove(currentHero);
        }
        public Hero GetHeroWithHighestStrength()
        {
            heroes = heroes.OrderByDescending(x => x.Item.Strength).ToList();
            return  heroes.FirstOrDefault();
        }
        public Hero GetHeroWithHighestAbility()
        {
            heroes = heroes.OrderByDescending(x => x.Item.Ability).ToList();
            return heroes.FirstOrDefault();
        }
        public Hero GetHeroWithHighestIntelligence()
        {
            heroes = heroes.OrderByDescending(x => x.Item.Intelligence).ToList();
            return heroes.FirstOrDefault();
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var hero in heroes)
            {
                builder.AppendLine(hero.ToString());
            }
            return builder.ToString().TrimEnd() ;
        }
    }
}
