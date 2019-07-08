using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingSpree
{
    public class Person
    {
        private string name;
        private decimal money;
        private List<Product> products;

        public Person(string name,decimal money)
        {
            this.Name = name;
            this.Money = money;
            this.Products = new List<Product>();
        }
        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    Exception ex = new ArgumentException("Name cannot be empty");
                    Console.WriteLine(ex.Message);
                    Environment.Exit(0);
                }
                name = value;
            }
        }
        public decimal Money
        {
            get => money;
            set
            {
                if (value < 0)
                {
                    Exception ex = new ArgumentException("Money cannot be negative");
                    Console.WriteLine(ex.Message);
                    Environment.Exit(0);
                }
                money = value;
            }
        }
        public List<Product> Products { get => products; set => products = value; }

        public void Add(Product product)
        {

            decimal cost = product.Cost;
            string productName = product.Name;
            if (cost > this.Money)
            {
                Console.WriteLine($"{this.Name} can't afford {productName}");
            }
            else
            {
                this.Products.Add(product);
                this.money -= cost;
                Console.WriteLine($"{this.Name} bought {productName}");
            }
        }
        public override string ToString()
        {
            if (products.Count == 0)
            {
                return $"{this.Name} - Nothing bought";
            }
            else
            {
                return $"{this.Name} - {string.Join(", ",products.Select(x => x.ToString()))}";
            }
        }
    }
}
