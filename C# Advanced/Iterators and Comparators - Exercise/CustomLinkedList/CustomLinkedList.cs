using System;

namespace CustomLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            var linkedList = new CoolLinkedList<int>();
            linkedList.AddHead(5);
            linkedList.AddHead(10);
            linkedList.AddHead(15);
            Console.WriteLine((int)linkedList.Head == 15);
            Console.WriteLine((int)linkedList.Tail == 5);
            Console.WriteLine(linkedList.Count == 3);

            linkedList.AddTail(20);
            linkedList.AddTail(25);

            linkedList.ForEach(Console.WriteLine, true);
            var arr = linkedList.ToArray();

            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine((int)linkedList.Head == 15);
            Console.WriteLine((int)linkedList.Tail == 25);
            Console.WriteLine(linkedList.Count == 5);

            Console.WriteLine((int)linkedList.RemoveHead() == 15);
            Console.WriteLine((int)linkedList.RemoveHead() == 10);
            Console.WriteLine((int)linkedList.Head == 5);
            Console.WriteLine((int)linkedList.Count == 3);

            Console.WriteLine((int)linkedList.RemoveTail() == 25);
            Console.WriteLine((int)linkedList.RemoveTail() == 20);
            Console.WriteLine((int)linkedList.RemoveTail() == 5);
            Console.WriteLine((int)linkedList.Count == 0);
            try
            {
                Console.WriteLine(linkedList.Head);
                Console.WriteLine(false);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine(true);
            }
            linkedList = new CoolLinkedList<int>();
            linkedList.AddTail(5);
            linkedList.AddTail(10);
            linkedList.AddTail(5);
            linkedList.AddTail(20);
            linkedList.AddTail(5);

            linkedList.Remove(5);

            Console.WriteLine((int)linkedList.Head == 10);
            Console.WriteLine((int)linkedList.Tail == 20);
            Console.WriteLine(linkedList.Contains(10));
            Console.WriteLine(linkedList.Contains(20));
            Console.WriteLine(linkedList.Contains(5) == false);
            Console.WriteLine(linkedList.Count == 2);

            linkedList.Clear();
            Console.WriteLine(linkedList.Count == 0);
            Console.WriteLine(linkedList.Contains(10) == false);
            Console.WriteLine(linkedList.Contains(20) == false);
            linkedList.AddTail(1234);
            linkedList.AddTail(5678);
            linkedList.AddTail(13579);
            linkedList.AddTail(2468);

            foreach (var item in linkedList)
            {
                Console.WriteLine(item);
            }
        }
    }
}
