using Ardalis.ApiEndpoints;
using Azure;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace FoodDelivery.Endpoints.Restaurants
{
    public class DeleteRestaurantRequest
    {

        public int Id { get; set; }


    }

    public class DeleteRestaurant : EndpointBaseAsync
       .WithoutRequest
        .WithResult<RestaurantModel>
    {
        private readonly IRestaurantRepo _restaurant;

        public DeleteRestaurant(IRestaurantRepo restaurant)
        {
            _restaurant = restaurant;
        }

        [HttpDelete("restaurants/{id:int}")]
        [SwaggerOperation(Summary = "Delete a restaurant",
            Description = "Delete a restaurant",
            OperationId = "Restaurant.Delete",
            Tags = new[] { "RestaurantEndpoint" })]
        public override async Task<RestaurantModel> HandleAsync(CancellationToken cancellationToken = default)
        {
            int id = int.Parse(HttpContext.Request.RouteValues["id"].ToString());

            var restaurant = await _restaurant.GetRestaurant(id);
            if (restaurant is null)
                return null;
            await _restaurant.DeleteRestaurant(id);
            return null;


           
        }
    }
}
