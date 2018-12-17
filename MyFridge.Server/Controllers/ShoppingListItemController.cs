using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyFridge.Server.Extensions;
using MyFridge.Shared;

namespace MyFridge.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingListItemController : ControllerBase
    {
        Data.MyFridgeContext _context;

        public ShoppingListItemController(Data.MyFridgeContext context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public List<ShoppingListItem> All()
        {
            return _context.ShoppingItems.Select(r => r.ToShared()).ToList();
        }

        [HttpPost("[action]")]
        public List<ShoppingListItem> Add([FromBody] ShoppingListItem shoppingListItem)
        {
            _context.Add(shoppingListItem.ToData());
            _context.SaveChanges();

            return this.All();
        }

        [HttpGet("[action]/{ItemId}")]
        public ShoppingListItem Update(long itemId)
        {
            var currentItem = _context.ShoppingItems.FirstOrDefault(i => i.Id == itemId);
            currentItem.WasBought = !currentItem.WasBought;
            _context.SaveChanges();

            return currentItem.ToShared();
        }

        [HttpGet("[action]")]
        public List<ShoppingListItem> Remove()
        {
            var removeItems = _context.ShoppingItems;

            if (removeItems != null)
            {
                _context.RemoveRange(removeItems);
                _context.SaveChanges();
            }

            return this.All();
        }
    }
}