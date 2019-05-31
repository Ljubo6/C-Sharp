namespace _04.Froggy
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    public class Lake : IEnumerable<int>
    {
        private int[] stones;

        public Lake(int[] stones)
        {
            this.stones = stones;
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < this.stones.Length; i+= 2)
            {
                yield return this.stones[i];
            }
            int lastOddIndex = (this.stones.Length - 1) % 2 == 0 ? this.stones.Length - 2 : this.stones.Length - 1;
            for (int i = lastOddIndex; i > 0; i -= 2)
            {
                yield return this.stones[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
