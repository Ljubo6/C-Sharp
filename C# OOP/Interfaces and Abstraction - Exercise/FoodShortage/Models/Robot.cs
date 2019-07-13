using FoodShortage.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShortage.Models 
{
    public class Robot : IIdentifiable
    {
        private string id;
        private string model;
        public Robot(string model,string id)
        {
            this.Model = model;
            this.Id = id;
        }
        public string Id
        {
            get => id;
            private set => id = value;
        }
        public string Model
        {
            get => model;
            private set => model = value;
        }
    }
}
