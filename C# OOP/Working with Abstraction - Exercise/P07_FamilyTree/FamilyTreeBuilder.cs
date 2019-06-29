using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P07_FamilyTree
{
    public class FamilyTreeBuilder
    {
        public FamilyTreeBuilder(string mainPersonInput)
        {
            this.FamilyTree = new List<Person>();
            this.MainPerson = Person.CreatePerson(mainPersonInput);
            this.FamilyTree.Add(this.MainPerson);
        }
        public string Build()
        {
            StringBuilder resultBuilder = new StringBuilder();
            resultBuilder.AppendLine(this.MainPerson.ToString());
            resultBuilder.AppendLine("Parents:");
            foreach (var parent in this.MainPerson.Parents)
            {
                resultBuilder.AppendLine(parent.ToString());
            }
            resultBuilder.AppendLine("Children:");
            foreach (var child in this.MainPerson.Children)
            {
                resultBuilder.AppendLine(child.ToString());
            }
            return resultBuilder.ToString().TrimEnd();
        }

        private Person FindOrCreate(string name,string birthday = "")
        {
            if (birthday == "")
            {
                birthday = name;
            }
            var person = this.FamilyTree
                       .FirstOrDefault(c => c.Name == name || c.Birthday == name);

            if (person == null)
            {
                person = Person.CreatePerson(name);
                this.FamilyTree.Add(person);

            }
            return person;
        }
        public  void SetParentChildRelation(string parentInput, string childInput)
        {
            Person parent = FindOrCreate(parentInput);
            SetChild( parent, childInput);
        }
        private  void SetChild( Person parent, string childInput)
        {
            var child = FindOrCreate(childInput);

            parent.Children.Add(child);
            child.Parents.Add(parent);

        }
        public  void SetFullInfo( string name, string birthday)
        {
            var person = this.FamilyTree
                    .FirstOrDefault(p => p.Name == name || p.Birthday == birthday);
            if (person == null)
            {
                person = new Person();
                this.FamilyTree.Add(person);
            }
            person.Name = name;
            person.Birthday = birthday;

            this.CheckForDuplicate( person);

        }
        private  void CheckForDuplicate( Person person)
        {
            string name = person.Name;
            string birthday = person.Birthday;
            Person duplicate = this.FamilyTree
                .Where(p => p.Name == name || p.Birthday == birthday)
                .Skip(1)
                .FirstOrDefault();
            if (duplicate != null)
            {
                this.RemoveDuplicate(person, duplicate);
            }
        }
        private  void RemoveDuplicate(Person person, Person duplicate)
        {
            this.FamilyTree.Remove(duplicate);
            person.Parents.AddRange(duplicate.Parents);
            foreach (var parent in duplicate.Parents)
            {
                ReplaceDuplicate(person, duplicate, parent.Children);

            }

            person.Children.AddRange(duplicate.Children);
            foreach (var child in duplicate.Children)
            {
                ReplaceDuplicate(person, duplicate, child.Parents);
            }
        }
        private static void ReplaceDuplicate(Person original, Person duplicate, List<Person> collection)
        {
            int duplicateIndex = collection.IndexOf(duplicate);
            if (duplicateIndex > -1)
            {
                collection[duplicateIndex] = original;
            }
            else
            {
                collection.Add(original);
            }
        }
        public List<Person> FamilyTree { get; set; }
        public Person MainPerson { get; set; }
    }
}
