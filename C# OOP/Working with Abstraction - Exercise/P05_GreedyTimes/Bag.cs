using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P05_GreedyTimes
{
    public class Bag
    {
        private Dictionary<string, Dictionary<string, long>> bagSpace;
        public Bag()
        {
            this.BagSpace = new Dictionary<string, Dictionary<string, long>>();
        }
        public Dictionary<string, Dictionary<string, long>> BagSpace
        {
            get { return bagSpace; }
            set { bagSpace = value; }
        }

        public void FillBag(string[] seif, long bagCapacity)
        {
            long goldQuantity = 0;
            long gemQuantity = 0;
            long cashQuantity = 0;
            for (int i = 0; i < seif.Length; i += 2)
            {
                string itemName = seif[i];
                long itemAmount = long.Parse(seif[i + 1]);
                string itemType = FindItem(itemName);

                if (itemType == "")
                {
                    continue;
                }
                else if (bagCapacity < BagSpace.Values.Select(x => x.Values.Sum()).Sum() + itemAmount)
                {
                    continue;
                }

                switch (itemType)
                {
                    case "Gem":
                        if (!BagSpace.ContainsKey(itemType))
                        {
                            if (BagSpace.ContainsKey("Gold"))
                            {
                                if (itemAmount > BagSpace["Gold"].Values.Sum())
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else if (BagSpace[itemType].Values.Sum() + itemAmount > BagSpace["Gold"].Values.Sum())
                        {
                            continue;
                        }
                        break;
                    case "Cash":
                        if (!BagSpace.ContainsKey(itemType))
                        {
                            if (BagSpace.ContainsKey("Gem"))
                            {
                                if (itemAmount > BagSpace["Gem"].Values.Sum())
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else if (BagSpace[itemType].Values.Sum() + itemAmount > BagSpace["Gem"].Values.Sum())
                        {
                            continue;
                        }
                        break;
                }

                InitializeBagSpaceDictionary(itemName, itemAmount, itemType);
                CalqulateItemQuantity(ref goldQuantity, ref gemQuantity, ref cashQuantity, itemAmount, itemType);
            }
        }

        private void InitializeBagSpaceDictionary(string itemName, long itemAmount, string itemType)
        {
            if (!BagSpace.ContainsKey(itemType))
            {
                BagSpace[itemType] = new Dictionary<string, long>();
            }

            if (!BagSpace[itemType].ContainsKey(itemName))
            {
                BagSpace[itemType][itemName] = 0;
            }

            BagSpace[itemType][itemName] += itemAmount;
        }

        private static void CalqulateItemQuantity(ref long goldQuantity, ref long gemQuantity, ref long cashQuantity, long itemAmount, string itemType)
        {
            if (itemType == "Gold")
            {
                goldQuantity += itemAmount;
            }
            else if (itemType == "Gem")
            {
                gemQuantity += itemAmount;
            }
            else if (itemType == "Cash")
            {
                cashQuantity += itemAmount;
            }
        }

        private static string FindItem(string itemName)
        {
            string itemType = string.Empty;

            if (itemName.Length == 3)
            {
                itemType = "Cash";
            }
            else if (itemName.ToLower().EndsWith("gem"))
            {
                itemType = "Gem";
            }
            else if (itemName.ToLower() == "gold")
            {
                itemType = "Gold";
            }

            return itemType;
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var bag in BagSpace)
            {
                builder.AppendLine($"<{bag.Key}> ${bag.Value.Values.Sum()}");
                foreach (var item2 in bag.Value.OrderByDescending(y => y.Key).ThenBy(y => y.Value))
                {
                    builder.AppendLine($"##{item2.Key} - {item2.Value}");
                }
            }

            return builder.ToString().TrimEnd();
        }
    }
}
