using CollectionHierarchy.Core;
using CollectionHierarchy.Models;
using System;

namespace CollectionHierarchy
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var addCollection = new AddCollection();
            var addRemoveCollection = new AddRemoveCollection();
            var myList = new MyList();
            Engine engine = new Engine(addCollection,addRemoveCollection,myList);
            engine.Run();
        }
    }
}
