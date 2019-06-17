namespace HealthyHeaven
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public class Salad
    {
        private List<Vegetable> produsts;
        public Salad(string name)
        {
            this.Name = name;
            this.produsts = new List<Vegetable>();
        }
        public string Name { get; set; }

        public int GetTotalCalories()
        {
            return this.produsts.Sum(x => x.Calories);
        }
        public int GetProductCount()
        {
            return this.produsts.Count();
        }
        public void Add(Vegetable product)
        {
            this.produsts.Add(product);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"* Salad {this.Name} is {GetTotalCalories()} calories and have {GetProductCount()} products:");
            sb.Append(string.Join(Environment.NewLine, this.produsts));
            return sb.ToString();
        }
    }
}
