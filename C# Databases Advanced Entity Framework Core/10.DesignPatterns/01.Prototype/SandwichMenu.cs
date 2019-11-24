using System;
using System.Collections.Generic;
using System.Text;

namespace _01.Prototype
{
    class SandwichMenu
    {
        private Dictionary<string, SandwichPrototype> _sandwiches = new Dictionary<string, SandwichPrototype>();
        public SandwichPrototype this[string name] 
        {
            get { return _sandwiches[name]; }
            set { _sandwiches.Add(name, value); } 
        }
    }
}
