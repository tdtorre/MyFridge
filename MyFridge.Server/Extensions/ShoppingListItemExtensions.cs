namespace MyFridge.Server.Extensions
{
    public static class ShoppingListItemExtensions
    {
        public static Shared.ShoppingListItem ToShared(this Data.Model.ShoppingItem f)
        {
            return new Shared.ShoppingListItem()
            {
                Id = f.Id,
                Name = f.Name,
                WasBought = f.WasBought
            };
        }

        public static Data.Model.ShoppingItem ToData(this Shared.ShoppingListItem f)
        {
            return new Data.Model.ShoppingItem()
            {
                Id = f.Id,
                Name = f.Name,
                WasBought = f.WasBought
            };
        }
    }
}
