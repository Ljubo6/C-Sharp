using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _04._MeTube_Statistics
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            Dictionary<string, List<int>> video = new Dictionary<string, List<int>>();

            while ((input = Console.ReadLine()) != "stats time")
            {
                if (input.Contains('-'))
                {
                    string[] tokens = input.Split('-');
                    string videoName = tokens[0];
                    int view = int.Parse(tokens[1]);
                    if (!video.ContainsKey(videoName))
                    {
                        video.Add(videoName, new List<int>());
                        video[videoName].Add(view);
                        video[videoName].Add(0);
                    }
                    else
                    {
                        video[videoName][0] += view;
                    }
                }
                else if (input.Contains(':'))
                {
                    string[] tokens = input.Split(':');
                    string command = tokens[0];
                    string videoName = tokens[1];
                    if (command == "like")
                    {
                        if (video.ContainsKey(videoName))
                        {
                            video[videoName][1]++;
                        }

                    }
                    else if (command == "dislike")
                    {
                        if (video.ContainsKey(videoName))
                        {
                            video[videoName][1]--;
                        }
                    }
                }
            }
            string criterion = Console.ReadLine();
            if (criterion == "by views")
            {
                foreach (var kvp in video.OrderByDescending(x => x.Value[0]))
                {
                    Console.WriteLine("{0} - {1} views - {2} likes", kvp.Key, kvp.Value[0], kvp.Value[1]);
                }
            }
            else if (criterion == "by likes")
            {
                foreach (var kvp in video.OrderByDescending(x => x.Value[1]))
                {
                    Console.WriteLine("{0} - {1} views - {2} likes", kvp.Key, kvp.Value[0], kvp.Value[1]);
                }
            }

        }
    }
}
