using Ardalis.ApiEndpoints;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.Restaurants
{
    public class GetAllRestaurants : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<List<RestaurantModel>>
    {

        private readonly IRestaurantRepo _restaurant;
        
        public GetAllRestaurants(IRestaurantRepo restaurant)
        {
            _restaurant = restaurant;
        }
        [HttpGet("restaurants")]
        [SwaggerOperation(Summary = "Get all restaurants",
            Description ="Get all restaurants",
            OperationId ="Restaurant.GetAll",
            Tags = new[] {"RestaurantEndpoint"})]
        public override async Task<ActionResult<List<RestaurantModel>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var restaurants = await _restaurant.GetRestaurants();
            return Ok(restaurants);
        }
    }
}
