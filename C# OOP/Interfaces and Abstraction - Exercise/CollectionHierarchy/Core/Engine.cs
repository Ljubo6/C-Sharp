using CollectionHierarchy.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionHierarchy.Core
{
    public class Engine
    {
        private readonly IAddCollection addCollection;
        private readonly IAddRemoveCollection addRemoveCollection;
        private readonly IMyList myList;
        public Engine(IAddCollection addCollection, IAddRemoveCollection addRemoveCollection, IMyList myList)
        {
            this.addCollection = addCollection;
            this.addRemoveCollection = addRemoveCollection;
            this.myList = myList;
        }
        public void Run()
        {
            string[] input = Console.ReadLine().Split();
            int removeOperationCount = int.Parse(Console.ReadLine());
            this.PrintResult(input,removeOperationCount);
            
        }

        private void PrintResult(string[] input, int removeOperationCount)
        {
           this.PrintAddedResults(input,this.addCollection);
           this.PrintAddedResults(input,this.addRemoveCollection);
           this.PrintAddedResults(input,this.myList);

            this.PrintRemovedResults(removeOperationCount,this.addRemoveCollection);
            this.PrintRemovedResults(removeOperationCount,this.myList);
        }

        private void PrintRemovedResults(int removeOperationCount, IAddRemoveCollection collection)
        {
            var removedResults = new List<string>();
            for (int j = 0; j < removeOperationCount; j++)
            {
                removedResults.Add(collection.Remove());
            }
            Console.WriteLine(string.Join(" ",removedResults));
        }

        private void PrintAddedResults(string[] input, IAddCollection collection)
        {
            var addedResults = new List<int>();
            foreach (var text in input)
            {
                addedResults.Add(collection.Add(text));
            }
            Console.WriteLine(string.Join(" ",addedResults));
        }
    }
}
