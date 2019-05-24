namespace _04.OpinionPoll
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    class Humans
    {
        private List<Person> people;
        public Humans()
        {
            this.People = new List<Person>();
        }
        public List<Person> People
        {
            get { return people; }
            set { people = value; }
        }
        public void AddMembers(Person member)
        {
            if (member == null)
            {
                throw new Exception();
            }
            this.People.Add(member);
        }
        public List<Person> GetOldestCollectionOver30()
        {
            return this.People.Where(x => x.Age > 30).OrderBy(x => x.Name).ToList();
        }
    }
}
