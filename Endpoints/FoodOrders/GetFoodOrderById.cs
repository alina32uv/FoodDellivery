using Ardalis.ApiEndpoints;
using Azure;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.FoodOrders
{
    public class GetFoodOrderById : EndpointBaseAsync
       .WithoutRequest
        .WithResult<FoodOrdersModel>
    {
        private readonly IFoodOrdersRepo _food;

        public GetFoodOrderById(IFoodOrdersRepo food)
        {
            _food = food;
        }

        [HttpGet("food_orders/{id:int}", Name= "FoodOrderById")]
        [SwaggerOperation(Summary = "Get a food order by id",
            Description = "Get a food order by id",
            OperationId = "FoodOrder.GetById",
            Tags = new[] { "FoodOrderEndpoint" })]
        public override async  Task<FoodOrdersModel> HandleAsync(CancellationToken cancellationToken = default)
        {
            int id = int.Parse(HttpContext.Request.RouteValues["id"].ToString());

            var food = await _food.GetOrder(id);
            if (food is null)
                return null;
            return food;
        }
    }
}
