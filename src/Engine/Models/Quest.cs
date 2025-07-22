using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Quest
    {
        public int ID { get; }
        public string Name { get; }
        public string Description { get; }

        public List<ItemQuantity> ItemsToComplete { get; }

        public int RewardExperiencePoints { get; }
        public int RewardGold { get; }
        public List<ItemQuantity> RewardItems { get; }

        public string ToolTipContents =>
            Description + Environment.NewLine + Environment.NewLine +
            "To complete this quest, you must collect:" + Environment.NewLine +
            "=========================================" + Environment.NewLine +
            string.Join(Environment.NewLine, ItemsToComplete.Select(i => i.QuantityDescription)) +
            Environment.NewLine + Environment.NewLine +
            "Rewards\r\n" +
            "=========================================" + Environment.NewLine +
            $"{RewardExperiencePoints} experience points" + Environment.NewLine +
            "{RewardGold} gold" + Environment.NewLine +
            string.Join(Environment.NewLine, RewardItems.Select(i => i.QuantityDescription));

        public Quest(int iD, string name, string description, List<ItemQuantity> itemsToComplete,
                     int rewardExperiencePoints, int rewardGold, List<ItemQuantity> rewardItems)
        {
            ID = iD;
            Name = name;
            Description = description;
            ItemsToComplete = itemsToComplete;
            RewardExperiencePoints = rewardExperiencePoints;
            RewardGold = rewardGold;
            RewardItems = rewardItems;
        }
    }
}
