using System;

namespace MyFridge.Shared
{
    public class FridgeItem : Item
    {
        public int Quantity { get; set; }

        public FridgeLocation Location { get; set; }

        public bool IsActive { get; set; }
    }
}
