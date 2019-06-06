namespace CustomList
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class CustomListList
    {
        private const int InitialCapacity = 2;

        private int[] arr;
        public CustomListList()
        {
            this.arr = new int[InitialCapacity];
        }
        public int Count { get; private set; }
        public int[] Arr
        {
            get { return this.GetOnlyRealElements(); }
            private set { this.arr = value; }

        }

        public int this[int index]
        {
            get
            {
                if (index >= this.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return arr[index];
            }
            set
            {
                if (index >= this.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }
                arr[index] = value;
            }
        }
        private void Resize()
        {
            int[] copy = new int[this.arr.Length * 2];
            for (int i = 0; i < this.arr.Length; i++)
            {
                copy[i] = this.arr[i];
            }
            this.arr = copy;
        }
        public void Add(int item)
        {
            if (this.Count == this.arr.Length)
            {
                this.Resize();
            }
            this.arr[this.Count] = item;
            this.Count++;
        }
        private void Shift(int index)
        {
            for (int i = index; i < this.Count - 1; i++)
            {
                this.arr[i] = this.arr[i + 1];
            }
        }
        private void Shrink()
        {
            int[] copy = new int[this.arr.Length / 2];
            for (int i = 0; i < this.Count; i++)
            {
                copy[i] = this.arr[i];
            }
        }
        public int RemoveAt(int index)
        {
            if (index >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            var item = this.arr[index];
            this.arr[index] = default(int);
            this.Shift(index);

            this.Count--;
            if (this.Count <= this.arr.Length / 4)
            {
                this.Shrink();
            }

            return item;
        }
        public bool Contains (int item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (item.Equals(this.arr[i]))
                {
                    return true;
                }
            }
            return false;
        }
        public void Swap(int firstIndex,int secondIndex)
        {

            if (firstIndex < 0 
                || secondIndex < 0 
                || firstIndex >= this.Count 
                || secondIndex >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            int temp = this.arr[firstIndex];
            this.arr[firstIndex] = this.arr[secondIndex];
            this.arr[secondIndex] = temp;
        }
        private void ShiftToRight(int index)
        {
            for (int i = Count; i > index; i--)
            {
                this.arr[i] = this.arr[i - 1];
            }
        }
        public void Insert(int index,int element)
        {
            if (index > this.Count)
            {
                throw new IndexOutOfRangeException();
            }
            if (this.Count == this.arr.Length)
            {
                this.Resize();
            }
            this.ShiftToRight(index);
            this.arr[index] = element;
            this.Count++;
        }
        private int[] GetOnlyRealElements()
        {
            int[] temp = new int[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                temp[i] = arr[i];
            }
            return temp;
        }

    }
}
