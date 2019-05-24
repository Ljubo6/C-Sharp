namespace _12.Google
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class Person
    {
        public Person(string name)
        {
            this.Name = name;
            this.Company = null;
            this.Pokemons = new List<Pokemon>();
            this.Parents = new List<Parent>();
            this.Children = new List<Child>();
            this.Car = null;
        }
        public string Name { get; set; }
        public Company Company { get; set; }
        public List<Pokemon> Pokemons { get; set; }
        public List<Parent> Parents { get; set; }
        public List<Child> Children { get; set; }
        public Car Car { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{this.Name}");
            stringBuilder.AppendLine($"Company:");
            if (this.Company != null)
            {
                stringBuilder.AppendLine($"{this.Company}");
            }            
            stringBuilder.AppendLine($"Car:");
            if (this.Car != null)
            {
                stringBuilder.AppendLine($"{this.Car}");
            }            
            stringBuilder.AppendLine($"Pokemon:");
            if (Pokemons.Count != 0)
            {
                stringBuilder.AppendLine($"{string.Join("\n", Pokemons)}");
            }           
            stringBuilder.AppendLine($"Parents:");
            if (Parents.Count != 0)
            {
                stringBuilder.AppendLine($"{string.Join("\n", this.Parents)}");
            }            
            stringBuilder.AppendLine($"Children:");
            if (Children.Count != 0)
            {
                stringBuilder.Append($"{string.Join("\n", this.Children)}");
            }            
            return stringBuilder.ToString();
        }

    }
}
