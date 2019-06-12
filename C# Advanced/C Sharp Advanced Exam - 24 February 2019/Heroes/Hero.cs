using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes
{
    public class Hero
    {
        public Hero(string name,int level,Item item)
        {
            this.Name = name;
            this.Level = level;
            this.Item = item;
        }
        public string Name { get; set; }
        public int Level { get; set; }
        public Item Item { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine ($"Hero: {this.Name} – {this.Level}lvl");
            stringBuilder.Append(this.Item);
            return stringBuilder.ToString();
        }
    }
}
