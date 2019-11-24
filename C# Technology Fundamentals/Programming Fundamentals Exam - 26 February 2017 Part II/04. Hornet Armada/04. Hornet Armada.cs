using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._Hornet_Armada
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Dictionary<string, long> nameAndActivity = new Dictionary<string, long>();
            Dictionary<string, Dictionary<string,long>> nameAndTypeWithCount = new Dictionary<string, Dictionary<string,long>>();
            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split(new[] {" = "," -> ",":"},StringSplitOptions.RemoveEmptyEntries);
                long lastActivity = long.Parse(input[0]);
                string legionName = input[1];
                string soldierType = input[2];
                long soldierCount = long.Parse(input[3]);

                if (!nameAndActivity.ContainsKey(legionName))
                {
                    nameAndActivity.Add(legionName,0);
                }
                if (nameAndActivity[legionName] < lastActivity)
                {
                    nameAndActivity[legionName] = lastActivity;
                }
                if (!nameAndTypeWithCount.ContainsKey(legionName))
                {
                    nameAndTypeWithCount.Add(legionName,new Dictionary<string, long>());
                    
                }
                if (!nameAndTypeWithCount[legionName].ContainsKey(soldierType))
                {
                    nameAndTypeWithCount[legionName].Add(soldierType, 0);
                }
                nameAndTypeWithCount[legionName][soldierType] += soldierCount;
            }
            string[] command = Console.ReadLine().Split('\\');
            if (command.Length > 1)
            {
                Dictionary<string, long> legionNameAndSoldierCount = new Dictionary<string, long>();
                long activity = long.Parse(command[0]);
                string typeOfSoldiers = command[1];
                var editedNameAndActivity = nameAndActivity.Where(x => x.Value < activity).ToDictionary(x => x.Key,x => x.Value);
                foreach (var kvp in editedNameAndActivity)
                {
                    foreach (var KVP in nameAndTypeWithCount)
                    {
                        if (kvp.Key == KVP.Key)
                        {
                            var tempDict = KVP.Value;
                            foreach (var type in tempDict)
                            {
                                if (type.Key == typeOfSoldiers)
                                {
                                    legionNameAndSoldierCount.Add(kvp.Key, type.Value);
                                }
                                
                            }
                        }
                    }
                }
                foreach (var kvp in legionNameAndSoldierCount.OrderByDescending(x => x.Value))
                {
                    Console.WriteLine($"{kvp.Key} -> {kvp.Value}");
                }
            }
            else
            {
                string typeOfSoldiers = command[0];
                Dictionary<string, long> nameAndActivityWithType = new Dictionary<string, long>();
                foreach (var kvp in nameAndActivity)
                {
                    foreach (var KVP in nameAndTypeWithCount)
                    {
                        if (kvp.Key == KVP.Key)
                        {
                            var tempDict = KVP.Value;
                            foreach (var type in tempDict)
                            {
                                if (type.Key == typeOfSoldiers)
                                {
                                    nameAndActivityWithType.Add(kvp.Key, kvp.Value);
                                }

                            }
                        }
                    }
                }
                foreach (var kvp in nameAndActivityWithType.OrderByDescending(x => x.Value))
                {
                    Console.WriteLine($"{kvp.Value} : {kvp.Key}");
                }
            }
        }
    }
}
