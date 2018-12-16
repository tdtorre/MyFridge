using Microsoft.AspNetCore.Blazor;
using MyFridge.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyFridge.Client.Services
{
    public class AppState
    {
        private readonly HttpClient http;

        private IList<FridgeItem> AllFridgeItems;
        
        public bool InProgress { get; private set; }

        public IList<ShoppingListItem> ShoppingListItems { get; set; }

        public IList<FridgeItem> FridgeItems
        {
            get
            {
                if(this.AllFridgeItems != null && this.AllFridgeItems.Any(i => i.Location == FridgeLocation.Fridge))
                {
                    return this.AllFridgeItems.Where(f => f.Location == FridgeLocation.Fridge).ToList();
                }
                else
                {
                    return new List<FridgeItem>();
                }
            }
        }

        public IList<FridgeItem> FreezerItems
        {
            get
            {
                if (this.AllFridgeItems != null && this.AllFridgeItems.Any(i => i.Location == FridgeLocation.Freezer))
                {
                    return this.AllFridgeItems.Where(f => f.Location == FridgeLocation.Freezer).ToList();
                }
                else
                {
                    return new List<FridgeItem>();
                }
            }
        }

        public bool SearchInProgress { get; private set; }

        public event Action OnUdpatedFridgeItems;

        public AppState(HttpClient httpInstance)
        {
            this.http = httpInstance;
        }

        public async Task GetFridgeItems()
        {
            this.InProgress = true;
            NotifyStateChanged();

            this.AllFridgeItems = await http.GetJsonAsync<List<FridgeItem>>("/api/FridgeItem/All");
            this.InProgress = false;
            NotifyStateChanged();
        }

        public async Task GetShoppingListItems()
        {
            this.InProgress = true;
            NotifyStateChanged();

            this.ShoppingListItems = await http.GetJsonAsync<List<ShoppingListItem>>("/api/ShoppingListItem/All");
            this.InProgress = false;
            NotifyStateChanged();
        }

        public async Task AddToFridgelist(FridgeItem fridgeItem)
        {
            this.InProgress = true;
            NotifyStateChanged();

            this.AllFridgeItems = await http.PostJsonAsync<List<FridgeItem>>("/api/FridgeItem/Add", fridgeItem);
            this.InProgress = false;
            NotifyStateChanged();
        }

        public async Task AddToShoppingList(string shoppingListItemName)
        {
            if (!this.ShoppingListItems.Any(i => i.Name == shoppingListItemName))
            {
                var shoppingListItem = new ShoppingListItem() { Name = shoppingListItemName };
                this.ShoppingListItems = await http.PostJsonAsync<List<ShoppingListItem>>("/api/ShoppingListItem/Add", shoppingListItem);
                NotifyStateChanged();
            }
        }

        public async Task UpdateFridgeListItem(FridgeItem fridgeItem)
        {
            await http.PostJsonAsync<List<ShoppingListItem>>("/api/FridgeItem/Update", fridgeItem);
        }

        public async Task UpdateShoppingListItem(long shoppingListItemId)
        {
            await http.GetJsonAsync<List<ShoppingListItem>>($"/api/ShoppingListItem/Update/{shoppingListItemId}");
            var currentItem = this.ShoppingListItems.First(i => i.Id == shoppingListItemId);
            currentItem.WasBought = !currentItem.WasBought;
        }

        public async Task RemoveFromFridgelist(long fridgeItemId)
        {
            this.InProgress = true;
            NotifyStateChanged();

            this.AllFridgeItems = await http.GetJsonAsync<List<FridgeItem>>($"/api/FridgeItem/Remove/{fridgeItemId}");
            this.InProgress = false;
            NotifyStateChanged();
        }

        public async Task CleanShoppingList()
        {
            this.InProgress = true;
            NotifyStateChanged();

            foreach(var shoppingListItem in this.ShoppingListItems)
            {
                await http.GetJsonAsync<List<ShoppingListItem>>($"/api/ShoppingListItem/Remove/{shoppingListItem.Id}");
            }

            this.InProgress = false;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnUdpatedFridgeItems?.Invoke();
    }
}
