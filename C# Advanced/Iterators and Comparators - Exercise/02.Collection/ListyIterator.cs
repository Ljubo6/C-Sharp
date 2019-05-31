namespace _01.ListyIterator
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    public class ListyIterator<T> : IEnumerable<T>
    {
        private T[] elements;
        private int index;

        public ListyIterator(T[] elements)
        {
            this.elements = elements;
            this.index = 0;
        }

        public bool Move()
        {
            if (this.index < this.elements.Length - 1)
            {
                this.index++;
                return true;
            }
            return false;
        }
        public bool HasNext()
        {
            return this.index < this.elements.Length - 1 ? true : false;
        }
        public void Print()
        {
            if (this.elements.Length != 0)
            {
                Console.WriteLine(this.elements[this.index]);
                return;
            }
            throw new InvalidOperationException("Invalid Operation!");
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.elements.Length; i++)
            {
                yield return this.elements[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
