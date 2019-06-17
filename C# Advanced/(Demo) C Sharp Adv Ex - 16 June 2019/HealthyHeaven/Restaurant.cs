namespace HealthyHeaven
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public class Restaurant
    {
        private List<Salad> data;
        public Restaurant(string name)
        {
            this.Name = name;
            data = new List<Salad>();
        }
        public string Name { get; set; }
        public void Add(Salad salad)
        {
            this.data.Add(salad);
        }
        public bool Buy(string name)
        {
            var currentSalad = data.FirstOrDefault(x => x.Name == name);
            if (currentSalad == null)
            {
                return false;
            }
            else
            {
                data.Remove(currentSalad);
                return true;
            }
        }
        public Salad GetHealthiestSalad()
        {
            return this.data.OrderBy(x => x.GetTotalCalories()).FirstOrDefault();
        }
        public string GenerateMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.Name} have {this.data.Count} salads:");
            sb.Append(string.Join(Environment.NewLine, this.data));
            return sb.ToString();
        }
    }
}
