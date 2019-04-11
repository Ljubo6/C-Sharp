using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._Endurance_Rally
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] driversNames = Console.ReadLine().Split();
            List<double> trackLayoutZones = Console.ReadLine().Split().Select(double.Parse).ToList();
            List<int> checkpointIndex = Console.ReadLine().Split().Select(int.Parse).ToList();
            for (int i = 0; i < driversNames.Length; i++)
            {
                string driver = driversNames[i];
                int startingFuelCurrentDriver = (int)driver[0];
                double sum = startingFuelCurrentDriver;
                bool isFinished = true;
                for (int j = 0; j < trackLayoutZones.Count; j++)
                {
                    bool isCheckpoint = GetCheckpoint(j, checkpointIndex);
                    if (isCheckpoint)
                    {
                        sum += trackLayoutZones[j];
                    }
                    else
                    {
                        sum -= trackLayoutZones[j];
                        if (sum <= 0)
                        {
                            Console.WriteLine($"{driver} - reached {j}");
                            isFinished = false;
                            break;
                        }
                    }
                }

                if (isFinished)
                {
                    Console.WriteLine($"{driver} - fuel left {sum:F2}");
                }

            }
        }

        private static bool GetCheckpoint(int i, List<int> checkpointIndex)
        {
            bool currentCheck = false;
            for (int j = 0; j < checkpointIndex.Count; j++)
            {
                if (checkpointIndex[j] == i)
                {
                    currentCheck = true;
                }
            }

            return currentCheck;
        }
    }
}
