using CollectionHierarchy.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionHierarchy.Models
{
    public class AddRemoveCollection : Collection, IAddRemoveCollection
    {
        public virtual string Remove()
        {
            string item = this.Data[this.Data.Count - 1];
            this.Data.RemoveAt(this.Data.Count - 1);
            return item;
        }
    }
}
