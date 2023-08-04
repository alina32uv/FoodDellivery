using Ardalis.ApiEndpoints;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.FoodOrders
{
    public class GetAllFoodOrders : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<List<FoodOrdersModel>>
    {
        private readonly IFoodOrdersRepo _food;

        public GetAllFoodOrders(IFoodOrdersRepo food)
        {
            _food = food;
        }
        [HttpGet("food_orders")]
        [SwaggerOperation(Summary = "Gets all Food Orders",
            Description = "Gets all Food Orders",
            OperationId = "FoodOrder.GetAll",
            Tags = new[] { "FoodOrderEndpoint" })]

        public override async Task<ActionResult<List<FoodOrdersModel>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var food = await _food.GetOrders();
            return Ok(food);
        }
    }
}
