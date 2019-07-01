using System;
using System.Collections.Generic;
using System.Text;

namespace MordorsCruelPlan.Foods
{
    public class Cram : Food
    {
        private const int happiness = 2;
        public Cram() 
            : base(happiness)
        {
        }
    }
}
