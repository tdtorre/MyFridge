using System;

namespace MyFridge.Shared
{
    public class FridgeItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public FridgeLocation Location { get; set; }

        public bool IsActive { get; set; }
    }
}
