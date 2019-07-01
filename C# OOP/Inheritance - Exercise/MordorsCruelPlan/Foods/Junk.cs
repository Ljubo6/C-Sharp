using System;
using System.Collections.Generic;
using System.Text;

namespace MordorsCruelPlan.Foods
{
    public class Junk : Food
    {
        private const int happiness = -1;

        public Junk() 
            : base(happiness)
        {
        }
    }
}
