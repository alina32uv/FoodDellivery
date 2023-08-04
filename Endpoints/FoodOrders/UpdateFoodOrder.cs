using Ardalis.ApiEndpoints;
using Azure;
using FoodDelivery.Attributes;
using FoodDelivery.Dto;
using FoodDelivery.Endpoints.Restaurants;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.FoodOrders
{
    public class UpdateFoodOrderRequest
    {

        [FromBody] public FoodOrdersModel UpdatedOrder { get; set; } = default!;


    }
    public class UpdateFoodOrder : EndpointBaseAsync
        .WithRequest<UpdateFoodOrderRequest>
        .WithResult<FoodOrdersModel>
    {
        private readonly IFoodOrdersRepo _food;

        public UpdateFoodOrder(IFoodOrdersRepo food)
        {
            _food = food;
        }
        [HttpPut("food_orders/{id:int}")]
        [SwaggerOperation(Summary = "Update a food order",
            Description = "Update a food order",
            OperationId = "FoodOrder.Update",
            Tags = new[] { "FoodOrderEndpoint" })]
        public override async Task<FoodOrdersModel> HandleAsync([FromMultiSource]UpdateFoodOrderRequest request, CancellationToken cancellationToken = default)
        {
            int id = int.Parse(HttpContext.Request.RouteValues["id"].ToString());
            var order = await _food.GetOrder(id);
            if (order is null)
            {
                return null;
            }
            order.User_Id = request.UpdatedOrder.User_Id;


            await _food.UpdateOrder(id, order);

            return null;
        }
    }
}
