using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Factories;

namespace Engine.Models
{
    public class ItemQuantity
    {
        public int ItemID { get; }
        public int Quantity { get; }

        public string QuantityDescription =>
            $"{Quantity} {ItemFactory.ItemName(ItemID)}";

        public ItemQuantity(int itemId, int quantity) 
        {
            ItemID = itemId;
            Quantity = quantity;
        }
    }
}
