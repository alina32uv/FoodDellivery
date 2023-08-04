using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Interfaces;
using Microsoft.Identity.Client;
using FoodDelivery.Models;
using FoodDelivery.Dto;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace FoodDelivery.Controllers
{
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantRepo restaurantRepo;

        public RestaurantController(IRestaurantRepo restaurantRepo)
        {
            this.restaurantRepo = restaurantRepo;
        }


        /*[HttpGet]
        
        public async Task<IActionResult> GetRestaurants()
        {
            try
            {
                var restaurants = await restaurantRepo.GetRestaurants();
                return Ok(restaurants);



            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }



        }
        */
       [HttpGet("{id}", Name = "RestaurantById")]
        public async Task<IActionResult> GetRestaurant(int id)
        {
            var restaurant = await restaurantRepo.GetRestaurant(id);
            if (restaurant is null)
                return NotFound();
            return Ok(restaurant);


        }


        [HttpPost]
        public async Task<ActionResult> CreateRestaurant([FromBody]RestaurantForCreationDto restaurant)
        {
            var createdRestaurant = await restaurantRepo.CreateRestaurant(restaurant);
            return CreatedAtRoute("RestaurantById", new { id = createdRestaurant.Id }, createdRestaurant);
         

        }
        
        [HttpGet("menu/{id}")]
        public async Task<IActionResult> GetMenu(int id)
        {
            var menu = await restaurantRepo.GetMenu(id);
            if (menu is null)
                return NotFound();
            return Ok(menu);


        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] RestaurantsForUpdateDto restaurant)
        {
            var dbRestaurant = await restaurantRepo.GetRestaurant(id);
            if (dbRestaurant is null)
                return NotFound();
            await restaurantRepo.UpdateRestaurant(id, restaurant);
            return NoContent();
        }

    }
}
