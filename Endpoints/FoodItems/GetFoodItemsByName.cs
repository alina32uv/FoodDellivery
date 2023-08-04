using Ardalis.ApiEndpoints;
using Azure;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.FoodItems
{
    public class GetFoodItemsByName : EndpointBaseAsync
       .WithoutRequest
        .WithActionResult<FoodItemsModel>
    {
        private readonly IFoodItemsRepo _food;

        public GetFoodItemsByName(IFoodItemsRepo food)
        {
            _food = food;
        }
        [HttpGet("food_items/{Name}", Name = "FoodItemByName")]
        [SwaggerOperation(Summary = "Get Food Item by Name",
            Description = "Get Food Item by Name",
            OperationId = "FoodItems.GetByName",
            Tags = new[] { "FoodItemsEndpoint" })]
        public override async Task<ActionResult<FoodItemsModel>> HandleAsync(CancellationToken cancellationToken = default)
        {
            string name = HttpContext.Request.RouteValues["Name"].ToString();

            var food = await _food.GetItemName(name);
            if (food is null)
                return null;
            return Ok(food);
        }
    }
}
