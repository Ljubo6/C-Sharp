using System;
using System.Collections.Generic;
using System.Text;

namespace MordorsCruelPlan.Foods
{
    internal class Apple : Food
    {
        private const int happiness = 1;
        public Apple()
            : base(happiness)
        {
        }
    }
}
