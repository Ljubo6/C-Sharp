namespace _05.GenericCountMethodString
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class Box<T>
        where T : IComparable<T>
    {
        public Box(List<T> items)
        {
            this.Items = items;
        }
        public List<T> Items { get; set; }

        public void Swap(int index1,int index2)
        {
            T tempVar = this.Items[index1];
            this.Items[index1] = this.Items[index2];
            this.Items[index2] = tempVar;           
        }

        public int GetGreaterThan(T inputItem)
        {
            int count = 0;
            foreach (var item in this.Items)
            {
                if (item.CompareTo(inputItem) > 0)
                {
                    count++;
                }
            }
            return count;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in this.Items)
            {
                sb.AppendLine($"{item.GetType().FullName}: {item}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
