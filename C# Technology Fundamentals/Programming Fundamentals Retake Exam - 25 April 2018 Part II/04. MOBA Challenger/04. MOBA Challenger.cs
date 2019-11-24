using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._MOBA_Challenger
{
    class Program
    {
        static void Main(string[] args)
        {

            string input = string.Empty;
            Dictionary<string, Dictionary<string, int>> playerSkill = new Dictionary<string, Dictionary<string, int>>();
            while ((input = Console.ReadLine()) != "Season end")
            {
                if (input.Contains("->"))
                {
                    string[] tokens = input.Split(new[] { " -> " }, StringSplitOptions.RemoveEmptyEntries);
                    string player = tokens[0];
                    string position = tokens[1];
                    int skill = int.Parse(tokens[2]);

                    if (!playerSkill.ContainsKey(player))
                    {
                        playerSkill.Add(player,new Dictionary<string, int>());

                    }
                    if (!playerSkill[player].ContainsKey(position))
                    {
                        playerSkill[player].Add(position, 0);
                    }
                    
                    if (playerSkill[player][position] <= skill)
                    {
                        playerSkill[player][position] = skill;
                    }
                    
                }
                else if (input.Contains("vs"))
                {
                    string[] tokens = input.Split(new[] { " vs " }, StringSplitOptions.RemoveEmptyEntries);
                    string playerOne = tokens[0];
                    string playerTwo = tokens[1];
                    if (playerSkill.ContainsKey(playerOne) && playerSkill.ContainsKey(playerTwo))
                    {
                        var dictOne = playerSkill[playerOne];
                        var dictTwo = playerSkill[playerTwo];
                        foreach (var kvp in dictOne)
                        {
                            foreach (var KVP in dictTwo)
                            {
                                if (KVP.Key == kvp.Key)
                                {
                                    if (kvp.Value > KVP.Value)
                                    {
                                        playerSkill.Remove(playerTwo);
                                    }
                                    else if (kvp.Value < KVP.Value)
                                    {
                                        playerSkill.Remove(playerOne);
                                    }
                                }
                            }
                        }
                    }
                }                
            }
            foreach (var kvp in playerSkill.OrderByDescending(x => x.Value.Values.Sum()).ThenBy(x => x.Key))
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value.Values.Sum()} skill");
                var currentDict = kvp.Value;
                foreach (var KVP in currentDict.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
                {
                    Console.WriteLine($"- {KVP.Key} <::> {KVP.Value}");
                }
            }
        }
    }
}
