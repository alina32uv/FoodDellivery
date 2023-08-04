using Ardalis.ApiEndpoints;
using Azure;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;


namespace FoodDelivery.Endpoints.Restaurants
{

    public class GetMenu : EndpointBaseAsync
       .WithoutRequest
        .WithResult<List<Menu>>
    {
        private readonly IRestaurantRepo _restaurant;

        public GetMenu(IRestaurantRepo restaurant)
        {
            _restaurant = restaurant;
        }
        [HttpGet("restaurants/menu/{id:int}")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation(Summary = "Get menu",
            Description = "Get menu",
            OperationId = "Restaurant.GetMenu",
            Tags = new[] { "RestaurantEndpoint" })]
        public override async Task<List<Menu>> HandleAsync(CancellationToken cancellationToken = default)
        {
            int id = int.Parse(HttpContext.Request.RouteValues["id"].ToString());

            var restaurantMenus = await _restaurant.GetMenu(id);
            if (restaurantMenus is null)
                return null;

            return (List<Menu>)restaurantMenus;
        }
       
    }
}
