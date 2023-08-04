using Ardalis.ApiEndpoints;
using Azure;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.FoodOrders
{
    public class DeleteFoodOrder : EndpointBaseAsync
       .WithoutRequest
        .WithActionResult<FoodOrdersModel>
    {
        private readonly IFoodOrdersRepo _food;

        public DeleteFoodOrder(IFoodOrdersRepo food)
        {
            _food = food;
        }
        [HttpDelete("food_orders/{id:int}")]
        [SwaggerOperation(Summary = "Delete a food order",
            Description = "Delete a food order",
            OperationId = "FoodOrder.Delete",
            Tags = new[] { "FoodOrderEndpoint" })]
        public override async Task<ActionResult<FoodOrdersModel>> HandleAsync(CancellationToken cancellationToken = default)
        {
            int id = int.Parse(HttpContext.Request.RouteValues["id"].ToString());

            var food = await _food.GetOrder(id);
            if (food is null)
                return null;
            await _food.DeleteOrder(id);
            return Ok("This Food Order Was Deleted.");
        }
    }
}
