using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop
{
    public class Book
    {
        private string title;
        private string author;
        private decimal price;
        public Book(string author,string title, decimal price)
        {
            this.Author = author;
            this.Title = title;
            this.Price = price;
        }
        public string Author
        {
            get { return author; }
            protected set
            {
                string[] firstSecondName = value.Split();
                if (firstSecondName.Length > 1)
                {
                    string secondName = firstSecondName[1];
                    if (char.IsDigit(secondName[0]))
                    {
                        throw new ArgumentException("Author not valid!");
                    }
                }
                
                author = value;
            }
        }
        public string Title
        {
            get { return title; }
            protected set
            {
                if (value.Length < 3)
                {
                    throw new ArgumentException("Title not valid!");
                }
                title = value;
            }
        }
        public virtual decimal Price
        {
            get { return price; }
            protected set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Price not valid!");
                }
                price = value;
            }
        }
        public override string ToString()
        {
            var resultBuilder = new StringBuilder();
            resultBuilder.AppendLine($"Type: {this.GetType().Name}")
                .AppendLine($"Title: {this.Title}")
                .AppendLine($"Author: {this.Author}")
                .AppendLine($"Price: {this.Price:f2}");

            string result = resultBuilder.ToString().TrimEnd();
            return result;
        }

    }
}
