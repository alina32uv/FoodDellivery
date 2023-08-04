using Ardalis.ApiEndpoints;
using Azure;
using FoodDelivery.Dto;
using FoodDelivery.Endpoints.Restaurants;
using FoodDelivery.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.FoodOrders
{
    public class CreateFoodOrderRequest
    {

        [FromBody] public FoodOrdersForCreationDto CreatedOrder { get; set; } = default!;
    }

    public class CreateFoodOrder : EndpointBaseAsync
        .WithRequest<CreateFoodOrderRequest>
        .WithActionResult<FoodOrdersForCreationDto>
    {

        private readonly IFoodOrdersRepo _food;

        public CreateFoodOrder(IFoodOrdersRepo food)
        {
            _food = food;
        }

        [HttpPost("food_orders")]
        [SwaggerOperation(Summary = "Create a new food order",
            Description = "Create a new food order",
            OperationId = "FoodOrder.Create",
            Tags = new[] { "FoodOrderEndpoint" })]
        public override async Task<ActionResult<FoodOrdersForCreationDto>> HandleAsync(CreateFoodOrderRequest request, CancellationToken cancellationToken = default)
        {
            var createdOrder = await _food.CreateOrder(request.CreatedOrder);
            return CreatedAtRoute("FoodOrderById", new { id = createdOrder.Id }, createdOrder);
        }
    }
}
