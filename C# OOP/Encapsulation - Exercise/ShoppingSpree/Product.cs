using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingSpree
{
    public class Product
    {
        private string name;
        private decimal cost;

        public Product(string name,decimal cost)
        {
            this.Name = name;
            this.Cost = cost;
        }
        public string Name
        {
            get => this.name;
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    Exception ex = new ArgumentException("Name cannot be empty");
                    Console.WriteLine(ex.Message);
                    Environment.Exit(0);
                }
                this.name = value;
            }
        }
        public decimal Cost
        {
            get => cost;
            set
            {
                if (value < 0)
                {
                    Exception ex = new ArgumentException("Money cannot be negative");
                    Console.WriteLine(ex.Message);
                    Environment.Exit(0);
                }
                cost = value;
            }
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
