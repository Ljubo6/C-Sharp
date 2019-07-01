using System;
using System.Collections.Generic;
using System.Text;

namespace MordorsCruelPlan.Foods
{
    public class Melon : Food
    {
        private const int happiness = 1;
        public Melon() 
            : base(happiness)
        {
        }
    }
}
