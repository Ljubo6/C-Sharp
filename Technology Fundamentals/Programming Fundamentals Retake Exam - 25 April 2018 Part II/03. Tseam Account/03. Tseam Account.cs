using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._Tseam_Account
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> account = Console.ReadLine().Split(' ').ToList();
            string command = string.Empty;
            while ((command = Console.ReadLine()) != "Play!")
            {
                string[] tokens = command.Split();
                string order = tokens[0];
                string game = tokens[1];
                if (order == "Install")
                {
                    if (game.Contains('-'))
                    {
                        continue;
                    }
                    if (!account.Contains(game))
                    {
                        account.Add(game);
                    }

                }
                else if (order == "Uninstall")
                {
                    if (game.Contains('-'))
                    {
                        continue;
                    }
                    if (account.Contains(game))
                    {
                        account.Remove(game);
                    }
                }
                else if (order == "Update")
                {
                    if (game.Contains('-'))
                    {
                        continue;
                    }
                    if (account.Contains(game))
                    {
                        account.Remove(game);
                        account.Add(game);
                    }
                }
                else if (order == "Expansion")
                {
                    string[] tempArr = game.Split('-');
                    if (tempArr[0].Contains('-') || tempArr[1].Contains('-'))
                    {
                        continue;
                    }
                    if (account.Contains(tempArr[0]))
                    {
                        game = game.Replace('-', ':').ToString();
                        int index = account.IndexOf(tempArr[0]);
                        account.Insert(index + 1, game);
                    }
                }
            }

            Console.WriteLine(string.Join(" ", account));
        }
    }
}
