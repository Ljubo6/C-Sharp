using CollectionHierarchy.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionHierarchy.Models
{
    public class AddCollection : Collection
    {
        public override int Add(string item)
        {
            base.Add(item);
            return this.Data.Count - 1;
        }
    }
}
