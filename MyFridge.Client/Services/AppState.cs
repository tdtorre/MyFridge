using MyFridge.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFridge.Client.Services
{
    public class AppState
    {
        private IList<FridgeItem> AllFridgeItems;

        private IList<FridgeItem> ShoppingListItems;

        public IList<FridgeItem> FridgeItems
        {
            get
            {
                return this.AllFridgeItems.Where(f => f.Location == FridgeLocation.Fridge).ToList();
            }
        }

        public IList<FridgeItem> FreezerItems
        {
            get
            {
                return this.AllFridgeItems.Where(f => f.Location == FridgeLocation.Freezer).ToList();
            }
        }

        public bool SearchInProgress { get; private set; }

        public event Action OnChange;

        public async Task LoadItems()
        {
            var fridgeItems = new List<FridgeItem>();
            fridgeItems.Add(new FridgeItem() { Id = Guid.NewGuid(), Name = "Milk", Quantity = 3, Location = FridgeLocation.Fridge });
            fridgeItems.Add(new FridgeItem() { Id = Guid.NewGuid(), Name = "Eggs", Quantity = 12, Location = FridgeLocation.Fridge });
            fridgeItems.Add(new FridgeItem() { Id = Guid.NewGuid(), Name = "Meat balls", Quantity = 2, Location = FridgeLocation.Fridge });
            fridgeItems.Add(new FridgeItem() { Id = Guid.NewGuid(), Name = "Beer", Quantity = 7, Location = FridgeLocation.Fridge });
            fridgeItems.Add(new FridgeItem() { Id = Guid.NewGuid(), Name = "Salmon", Quantity = 3, Location = FridgeLocation.Freezer });
            fridgeItems.Add(new FridgeItem() { Id = Guid.NewGuid(), Name = "Tuna", Quantity = 1, Location = FridgeLocation.Freezer });
            fridgeItems.Add(new FridgeItem() { Id = Guid.NewGuid(), Name = "Ice cream", Quantity = 2, Location = FridgeLocation.Freezer });
            this.AllFridgeItems = fridgeItems;
        }

        public void AddToFridgelist(FridgeItem fridgeItem)
        {
            if (fridgeItem.Id == Guid.Empty)
            {
                fridgeItem.Id = Guid.NewGuid();
            }

            this.AllFridgeItems.Add(fridgeItem);
            NotifyStateChanged();
        }

        public void AddToShoppingList(FridgeItem fridgeItem)
        {
            if (this.ShoppingListItems == null)
            {
                this.ShoppingListItems = new List<FridgeItem>();
            }

            this.ShoppingListItems.Add(fridgeItem);
            NotifyStateChanged();
        }

        //public void SelectItem(Guid activeItemId)
        //{
        //    var currentSelected = this.AllFridgeItems.FirstOrDefault(i => i.IsActive);

        //    if(currentSelected.Id == activeItemId)
        //    {
        //        currentSelected.IsActive = false;
        //    }
        //    else
        //    {                
        //        foreach (var item in this.AllFridgeItems)
        //        {
        //            item.IsActive = false;
        //        }

        //        this.AllFridgeItems.FirstOrDefault(i => i.Id == activeItemId).IsActive = true;
        //    }
            
        //    NotifyStateChanged();
        //}

        //public void IncreaseQuantity(Guid activeItemId)
        //{
        //    var currentItem = this.AllFridgeItems.FirstOrDefault(i => i.Id == activeItemId);

        //    if (currentItem != null)
        //    {
        //        currentItem.Quantity++;
        //    }
        //}

        //public void DecreaseQuantity(Guid activeItemId)
        //{
        //    var currentItem = this.AllFridgeItems.FirstOrDefault(i => i.Id == activeItemId);

        //    if (currentItem != null && currentItem.Quantity > 0)
        //    {
        //        currentItem.Quantity--;
        //    }
        //}

        public void RemoveFromFridgelist(FridgeItem fridgeItem)
        {
            this.AllFridgeItems.Remove(fridgeItem);
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
