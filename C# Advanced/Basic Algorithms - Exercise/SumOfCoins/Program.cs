namespace SumOfCoins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SumOfCoins
    {
        public static void Main(string[] args)
        {
            var availableCoins = new[] { 1, 2, 5, 10, 20, 50 };
            var targetSum = 923;

            var selectedCoins = ChooseCoins(availableCoins, targetSum);

            Console.WriteLine($"Number of coins to take: {selectedCoins.Values.Sum()}");
            foreach (var selectedCoin in selectedCoins)
            {
                Console.WriteLine($"{selectedCoin.Value} coin(s) with value {selectedCoin.Key}");
            }
        }

        public static Dictionary<int, int> ChooseCoins(IList<int> coins, int targetSum)
        {
            int[] sortedCoins = coins.OrderByDescending(coin => coin).ToArray();
            Dictionary<int, int> result = new Dictionary<int, int>();

            int currentSum = 0;
            int coinIndex = 0;

            while (currentSum != targetSum && coinIndex < sortedCoins.Length)
            {
                int currentCoin = sortedCoins[coinIndex];

                int remainder = targetSum - currentSum;
                int numberOfCoins = remainder / currentCoin;

                if (currentSum + currentCoin <= targetSum)
                {
                    result.Add(currentCoin, numberOfCoins);
                    currentSum += numberOfCoins * currentCoin;

                }
                coinIndex++;
            }

            if (currentSum != targetSum)
            {
                throw new InvalidOperationException();
            }

            return result;
        }
    }
}