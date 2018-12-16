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
        public void Add([FromBody] ShoppingListItem shoppingListItem)
        {
            _context.Add(shoppingListItem.ToData());
            _context.SaveChanges();
        }

        [HttpGet("[action]")]
        public void Update(long shoppingListItemId)
        {
            var currentItem = _context.ShoppingItems.FirstOrDefault(i => i.Id == shoppingListItemId);
            currentItem.WasBought = !currentItem.WasBought;
            _context.SaveChanges();
        }

        [HttpGet("[action]/{ItemId}")]
        public void Remove(long itemId)
        {
            var removeItem = _context.ShoppingItems.FirstOrDefault(i => i.Id == itemId);

            if (removeItem != null)
            {
                _context.Remove(removeItem);
                _context.SaveChanges();
            }
        }
    }
}