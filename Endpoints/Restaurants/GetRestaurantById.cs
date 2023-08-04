using Ardalis.ApiEndpoints;
using Azure;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using FoodDelivery.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.Restaurants
{
    public class GetRestaurantByIdRequest
    {

        public int Id { get; set; }
       

    }
  
    public class GetRestaurantById : EndpointBaseAsync
       .WithoutRequest
        .WithResult< RestaurantModel>
    {
        private readonly IRestaurantRepo _restaurant;

        public GetRestaurantById(IRestaurantRepo restaurant)
        {
            _restaurant = restaurant;
        }
        [HttpGet("restaurants/{id:int}")]
        [SwaggerOperation(Summary = "Get a restaurant by id",
            Description = "Get a restaurant by id",
            OperationId = "Restaurant.GetById",
            Tags = new[] { "RestaurantEndpoint" })]
        public override async Task<RestaurantModel> HandleAsync(CancellationToken cancellationToken = default)
        {
            int id = int.Parse(HttpContext.Request.RouteValues["id"].ToString());

            var restaurant = await _restaurant.GetRestaurant(id);
            if (restaurant is null)
                return null;
            return restaurant;
        }

    
    }
}
