using System.ComponentModel.DataAnnotations;

namespace MyFridge.Data.Model
{
    public class ShoppingItem
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public bool WasBought { get; set; }
    }
}
