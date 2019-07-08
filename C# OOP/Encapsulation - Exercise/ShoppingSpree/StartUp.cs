using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingSpree
{
    class StartUp
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>();
            List<Product> products = new List<Product>();

            string[] inputPeople = Console.ReadLine().Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            string[] inputProduct = Console.ReadLine().Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);


            for (int i = 0; i < inputPeople.Length; i++)
            {
                string[] tokens = inputPeople[i].Split("=");
                string name = tokens[0];
                decimal money = decimal.Parse(tokens[1]);
                Person person = new Person(name,money);
                people.Add(person);
            }
            for (int i = 0; i < inputProduct.Length; i++)
            {
                string[] tokens = inputProduct[i].Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                string name = tokens[0];
                decimal cost = decimal.Parse(tokens[1]);

                Product product = new Product(name,cost);
                products.Add(product);
            }

            string input = string.Empty;
            while ((input = Console.ReadLine()) != "END")
            {
                string[] tokens = input.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                string person = tokens[0];
                string productName = tokens[1];
                Product product = products.First(p => p.Name == productName);

                people.First(p => p.Name == person).Add(product);
            }
            foreach (var person in people)
            {
                Console.WriteLine(person.ToString());
            }
        }
    }
}
