using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Interfaces;
using Microsoft.Identity.Client;
using FoodDelivery.Models;
using FoodDelivery.Dto;
using FoodDelivery.Repositories;

namespace FoodDelivery.Controllers
{
    [Route("api/fooditems")]
    [ApiController]
    public class FoodItemsController : ControllerBase
    {
        private readonly IFoodItemsRepo itemsRepo;

        public FoodItemsController(IFoodItemsRepo itemsRepo)
        {
            this.itemsRepo = itemsRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            try
            {
                var items = await itemsRepo.GetItems();
                return Ok(items);

            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("{id}", Name = "ItemsById")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await itemsRepo.GetItem(id);
            if (item is null)
                return NotFound();
            return Ok(item);


        }

        [HttpGet("search /{name}", Name = "ItemsByNme")]
        public async Task<IActionResult> GetItemName(string name)
        {
            var item = await itemsRepo.GetItemName(name);
            if (item is null)
                return NotFound();
            return Ok(item);


        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] FoodItemsForUpdateDto item)
        {
            var dbItem = await itemsRepo.GetItem(id);
            if (dbItem is null)
                return NotFound();
            await itemsRepo.UpdateItem(id, item);
            return NoContent();
        }

    }
}
