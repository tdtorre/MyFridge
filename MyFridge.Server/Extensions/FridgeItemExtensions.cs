namespace MyFridge.Server.Extensions
{
    public static class FridgeItemExtensions
    {
        public static Shared.FridgeItem ToShared(this Data.Model.FridgeItem f)
        {
            return new Shared.FridgeItem()
            {
                Id = f.Id,
                Name = f.Name,
                Quantity = f.Quantity,
                Location = (Shared.FridgeLocation)f.Location,
                IsActive = false
            };
        }
        public static Data.Model.FridgeItem ToData(this Shared.FridgeItem f)
        {
            return new Data.Model.FridgeItem()
            {
                Name = f.Name,
                Quantity = f.Quantity,
                Location = (int)f.Location
            };
        }
    }
}
