using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Bags
{
    public class Backpack : IBag
    {
        private IList<string> items;
        public Backpack()
        {
            this.items = new List<string>();
        }
        public ICollection<string> Items => this.items;
    }
}
