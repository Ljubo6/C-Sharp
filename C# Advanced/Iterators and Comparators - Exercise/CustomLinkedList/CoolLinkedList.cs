namespace CustomLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    public class CoolLinkedList<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        private class CoolNode
        {
            public CoolNode(T value)
            {
                this.Value = value;
            }
            public T Value { get; private set; }
            public CoolNode Next { get; set; }
            public CoolNode Previous { get; set; }
        }
        private CoolNode head;
        private CoolNode tail;
        public int Count { get; private set; }

        public T Head
        {
            get
            {
                this.ValidateIfListEmpty();
                return this.head.Value;
            }
        }
        public T Tail
        {
            get
            {
                this.ValidateIfListEmpty();
                return this.tail.Value;
            }
        }
        public void AddHead(T value)
        {
            var newNode = new CoolNode(value);
            if (this.Count == 0)
            {
                this.head = this.tail = newNode;
            }
            else
            {
                var oldHead = this.head;
                oldHead.Previous = newNode;
                newNode.Next = oldHead;
                this.head = newNode;

            }

            this.Count++;
        }
        public void AddTail(T value)
        {
            var newNode = new CoolNode(value);
            if (this.Count == 0)
            {
                this.head = this.tail = newNode;
            }
            else
            {
                var oldTail = this.tail;
                oldTail.Next = newNode;
                newNode.Previous = oldTail;
                this.tail = newNode;
            }
            this.Count++;
        }
        public T RemoveHead()
        {
            this.ValidateIfListEmpty();
            var value = this.head.Value;
            if (this.head == this.tail)
            {
                this.head = null;
                this.tail = null;
            }
            else
            {
                var newHead = this.head.Next;
                newHead.Previous = null;
                this.head.Next = null;
                this.head = newHead;
            }
            this.Count--;
            return value;
        }
        public bool Contains(T value)
        {
            var currentNode = this.head;
            while (currentNode != null)
            {
                if (currentNode.Value.Equals(value))
                {
                    return true;
                }
                currentNode = currentNode.Next;
            }
            return false;
        }
        public T RemoveTail()
        {
            this.ValidateIfListEmpty();
            var value = this.tail.Value;
            if (this.head == this.tail)
            {
                this.head = null;
                this.tail = null;
            }
            else
            {
                var newTail = this.tail.Previous;
                this.tail.Previous = null;
                newTail.Next = null;
                this.tail = newTail;
            }
            this.Count--;
            return value;
        }
        public void Remove(T value)
        {
            var currentNode = this.head;

            while (currentNode != null)
            {
                var nodeValue = currentNode.Value;
                if (nodeValue.Equals(value))
                {
                    this.Count--;
                    var prevNode = currentNode.Previous;
                    var nextNode = currentNode.Next;

                    if (prevNode != null)
                    {
                        prevNode.Next = nextNode;
                    }
                    if (nextNode != null)
                    {
                        nextNode.Previous = prevNode;
                    }
                    if (this.head == currentNode)
                    {
                        this.head = currentNode.Next;
                    }
                    if (this.tail == currentNode)
                    {
                        this.tail = prevNode;
                    }
                }

                currentNode = currentNode.Next;
            }
        }
        public void Clear()
        {
            this.head = null;
            this.tail = null;
            this.Count = 0;
        }
        public void ForEach(Action<T> action, bool reverse = false)
        {
            var currentNode = reverse
                ? this.tail
                : this.head;

            while (currentNode != null)
            {
                action(currentNode.Value);
                currentNode = reverse
                    ? currentNode.Previous
                    : currentNode.Next;
            }
        }
        public T[] ToArray()
        {
            var arr = new T[this.Count];
            var currentNode = this.head;
            var index = 0;
            while (currentNode != null)
            {
                arr[index] = currentNode.Value;
                index++;
                currentNode = currentNode.Next;
            }
            return arr;
        }
        private void ValidateIfListEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Cool linked list is empty.");
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode =  this.head;

            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
