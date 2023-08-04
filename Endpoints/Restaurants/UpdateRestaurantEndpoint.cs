using Ardalis.ApiEndpoints;
using Azure;
using FoodDelivery.Attributes;
using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.Restaurants
{
    public class UpdateRestaurantRequest
    {

        [FromBody] public RestaurantModel UpdatedRestaurant { get; set; } = default!;

   
    }

    public class UpdateRestaurantEndpoint : EndpointBaseAsync
        .WithRequest<UpdateRestaurantRequest>
        .WithResult<RestaurantModel>
    {
        private readonly IRestaurantRepo _restaurant;

        public UpdateRestaurantEndpoint(IRestaurantRepo restaurant)
        {
            _restaurant = restaurant;
        }


        [HttpPut("restaurants/{id:int}")]
        [SwaggerOperation(Summary = "Update a restaurant",
            Description = "Update a restaurant",
            OperationId = "Restaurant.Update",
            Tags = new[] { "RestaurantEndpoint" })]

        public override async Task<RestaurantModel> HandleAsync([FromMultiSource]UpdateRestaurantRequest request, CancellationToken cancellationToken = default)
        {
            int id = int.Parse(HttpContext.Request.RouteValues["id"].ToString());
            var restaurant = await _restaurant.GetRestaurant(id);
            if (restaurant is null)
            {
                return null;
            }
            restaurant.Name = request.UpdatedRestaurant.Name;


            await _restaurant.UpdateRestaurant(id, restaurant);

            return null;

        }
    }
}
