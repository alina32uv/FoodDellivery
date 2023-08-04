using Ardalis.ApiEndpoints;
using Azure;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.FoodItems
{
    public class GetFoodItemById : EndpointBaseAsync
       .WithoutRequest
        .WithActionResult<List<FoodItemsModel>>
    {
        private readonly IFoodItemsRepo _food;

        public GetFoodItemById(IFoodItemsRepo food)
        {
            _food = food;
        }
        [HttpGet("food_items/{id:int}", Name = "FoodItemById")]
        [SwaggerOperation(Summary = "Get Food Item by Category Id",
            Description = "Get Food Item by Category Id",
            OperationId = "FoodItems.GetById",
            Tags = new[] { "FoodItemsEndpoint" })]
        public override async Task<ActionResult<List<FoodItemsModel>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            int id = int.Parse(HttpContext.Request.RouteValues["id"].ToString());

            var food = await _food.GetItem(id);
            if (food is null)
                return null;
            return Ok(food);
        }
    }
}
