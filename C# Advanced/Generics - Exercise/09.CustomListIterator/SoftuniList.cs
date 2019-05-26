using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _08.CustomListSorter
{
    public class SoftuniList<T> : ISoftuniList<T>
        where T : IComparable<T>
    {
        private const int InitialCapacity = 4;
        private T[] array;

        public SoftuniList()
        {
            this.array = new T[InitialCapacity];
            this.Count = 0;
        }
        public int Count { get; private set; }
        public void Add(T element)
        {
            if (this.array.Length == this.Count)
            {
                this.Resize();
            }
            this.array[this.Count++] = element;
            
        }

        public bool Contains(T element)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.array[i].Equals(element))
                {
                    return true;
                }
            }
            return false;
        }

        public int CountGreaterThan(T element)
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
            int counter = 0;

            for (int i = 0; i < this.Count; i++)
            {
                
                if (this.array[i].CompareTo(element) > 0)
                {
                    counter++;
                }
            }
            return counter;
        }

        public T Max()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
            T maxValue = this.array[0];
            for (int i = 1; i < this.Count; i++)
            {
                if (this.array[i].CompareTo(maxValue) > 0)
                {
                    maxValue = this.array[i];
                }
            }
            return maxValue;
        }

        public T Min()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
            T minValue = this.array[0];
            for (int i = 1; i < this.Count; i++)
            {
                if (this.array[i].CompareTo(minValue) <  0)
                {
                    minValue = this.array[i];
                }
            }
            return minValue;
        }

        public T Remove(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new InvalidOperationException();
            }
            T element = this.array[index];

            this.array[index] = default(T);
            this.Count--;
            for (int i = index; i < this.Count; i++)
            {
                this.array[i] = this.array[i + 1];
            }

            if (this.array.Length != this.Count)
            {
                this.array[this.Count] = default(T);
            }
            return element;
        }


        public void Swap(int index1, int index2)
        {
            if (index1 < 0 || index1 >= this.Count
                && index2 < 0 || index2 >= this.Count)
            {
                throw new InvalidOperationException();
            }
            T tempVar = this.array[index1];
            this.array[index1] = this.array[index2];
            this.array[index2] = tempVar;

        }

        public void Sort()
        {
            for (int i = 0; i < this.Count; i++)
            {
                for (int j = i + 1; j < this.Count; j++)
                {
                    if (this.array[i].CompareTo(this.array[j]) > 0)
                    {
                        T tempVar = this.array[i];
                        this.array[i] = this.array[j];
                        this.array[j] = tempVar;
                    }
                }
            }
        }
        private void Resize()
        {
            T[] tempArray = new T[this.array.Length * 2];

            for (int  i = 0;  i < this.array.Length;  i++)
            {
                tempArray[i] = this.array[i];
            }

            this.array = tempArray;
        }

        public override string ToString()
        {
            return string.Join("\n",this.array.Take(this.Count)); ;
        }


    }
}
