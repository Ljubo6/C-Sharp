using System;
using System.Collections.Generic;
using System.Text;

namespace MordorsCruelPlan.Foods
{
    public class Mushrooms : Food
    {
        private const int happiness = -10;

        public Mushrooms() 
            : base(happiness)
        {
        }
    }
}
