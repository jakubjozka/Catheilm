using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Engine.Models
{
    public class Quest
    {
        public int ID { get; }
        [JsonIgnore]
        public string Name { get; }
        [JsonIgnore]
        public string Description { get; }

        [JsonIgnore]
        public List<ItemQuantity> ItemsToComplete { get; }

        [JsonIgnore]
        public int RewardExperiencePoints { get; }
        [JsonIgnore]
        public int RewardGold { get; }
        [JsonIgnore]
        public List<ItemQuantity> RewardItems { get; }

        [JsonIgnore]
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
