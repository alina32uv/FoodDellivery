using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Interfaces;
using Microsoft.Identity.Client;
using FoodDelivery.Models;
using FoodDelivery.Dto;
using FoodDelivery.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace FoodDelivery.Controllers
{
    [Route("api/foodorders")]
    [ApiController]
    public class FoodOrdersController : ControllerBase
    {

        private readonly IFoodOrdersRepo ordersRepo;

        public FoodOrdersController(IFoodOrdersRepo ordersRepo)
        {
            this.ordersRepo = ordersRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var orders = await ordersRepo.GetOrders();
                return Ok(orders);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("{id}", Name = "OrderById")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await ordersRepo.GetOrder(id);
            if (order is null)
                return NotFound();
            return Ok(order);


        }

        [HttpPost]
         public async Task<ActionResult> CreateOrder([FromBody] FoodOrdersForCreationDto order)
         {
             var createdOrders = await ordersRepo.CreateOrder(order);
             return CreatedAtRoute("OrderById", new { id = createdOrders.Id }, createdOrders);

         }


        
        [Authorize(Roles = "admin,driver")]
        [HttpPut("setdriver/{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] FoodOrdersForUpdateDto order)
        {
            var dbOrder = await ordersRepo.GetOrder(id);
            if (dbOrder is null)
                return NotFound();
            await ordersRepo.UpdateOrder(id, order);
            return NoContent();
        }


        [HttpDelete ("{id}")]
        [Authorize(Roles = "AdminsAndDrivers")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var dbOrder = await ordersRepo.GetOrder(id);
            if (dbOrder is null)
                return NotFound();
            await ordersRepo.DeleteOrder(id);
            return NoContent();

        }
         

    }
}
