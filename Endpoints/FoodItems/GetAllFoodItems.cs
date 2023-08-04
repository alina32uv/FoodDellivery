using Ardalis.ApiEndpoints;
using Azure;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.FoodItems
{
    public class GetAllFoodItems : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<List<FoodItemsModel>>
    {
        private readonly IFoodItemsRepo _food;

        public GetAllFoodItems(IFoodItemsRepo food)
        {
            _food = food;
        }
        [HttpGet("food_items")]
        [SwaggerOperation(Summary = "Gets all Food Items",
            Description = "Gets all Food Items",
            OperationId = "FoodItems.GetAll",
            Tags = new[] { "FoodItemsEndpoint" })]

        public override async Task<ActionResult<List<FoodItemsModel>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var food = await _food.GetItems();
            return Ok(food);
        }
    }
}
