using Ardalis.ApiEndpoints;
using Azure;
using FoodDelivery.Attributes;
using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using FoodDelivery.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.Restaurants
{
    public class CreateRestaurantRequest
    {

        [FromBody] public RestaurantForCreationDto CreatedRestaurant { get; set; } = default!;


    }

    public class CreateRestaurant : EndpointBaseAsync
        .WithRequest<CreateRestaurantRequest>
        .WithActionResult<RestaurantForCreationDto>
    {
        private readonly IRestaurantRepo _restaurant;

        public CreateRestaurant(IRestaurantRepo restaurant)
        {
            _restaurant = restaurant;
        }
        [HttpPost("restaurants")]
        [SwaggerOperation(Summary = "Create a restaurant",
            Description = "Create a restaurant",
            OperationId = "Restaurant.Create",
            Tags = new[] { "RestaurantEndpoint" })]
        public override async Task<ActionResult<RestaurantForCreationDto>> HandleAsync(CreateRestaurantRequest request, CancellationToken cancellationToken = default)
{

    var createdRestaurant = await _restaurant.CreateRestaurant(request.CreatedRestaurant);
    return CreatedAtRoute("RestaurantById", new { id = createdRestaurant.Id }, createdRestaurant);
  


}
    }
}
