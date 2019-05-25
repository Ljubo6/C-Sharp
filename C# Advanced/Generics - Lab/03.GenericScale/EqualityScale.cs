namespace GenericScale
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class EqualityScale<T>
        where T : IComparable<T>
    {
        private T first;
        private T second;

        public EqualityScale(T first, T second)
        {
            this.first = first;
            this.second = second;
        }

        public bool AreEqual()
        {
            bool result = this.first.Equals(this.second);
            return result;
        }
    }
}
