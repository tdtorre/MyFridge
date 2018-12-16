using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyFridge.Server.Extensions;
using MyFridge.Shared;

namespace MyFridge.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FridgeItemController : ControllerBase
    {
        Data.MyFridgeContext _context;

        public FridgeItemController(Data.MyFridgeContext context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public List<FridgeItem> All()
        {
            return _context.FridgeItems.Select(r => r.ToShared()).ToList();
        }

        [HttpPost("[action]")]
        public List<FridgeItem> Add([FromBody] FridgeItem fridgeItem)
        {
            _context.Add( fridgeItem.ToData() );
            _context.SaveChanges();
            return _context.FridgeItems.Select(r => r.ToShared()).ToList();
        }

        [HttpPost("[action]")]
        public void Update([FromBody] FridgeItem fridgeItem)
        {
            var currentItem = _context.FridgeItems.FirstOrDefault(i => i.Id == fridgeItem.Id);
            currentItem.Quantity = fridgeItem.Quantity;
            _context.SaveChanges();
        }

        [HttpGet("[action]/{ItemId}")]
        public List<FridgeItem> Remove(long itemId)
        {
            var removeItem = _context.FridgeItems.FirstOrDefault(i => i.Id == itemId);
            
            if (removeItem != null)
            {
                _context.Remove(removeItem);
                _context.SaveChanges();
            }

            return _context.FridgeItems.Select(r => r.ToShared()).ToList();
        }
    }
}