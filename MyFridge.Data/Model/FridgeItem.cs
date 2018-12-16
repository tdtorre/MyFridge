using System.ComponentModel.DataAnnotations;

namespace MyFridge.Data.Model
{
    public class FridgeItem
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public int Location { get; set; }
    }
}
