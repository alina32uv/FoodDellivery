using Ardalis.ApiEndpoints;
using Azure;
using Dapper;
using FoodDelivery.Attributes;
using FoodDelivery.Endpoints.FoodOrders;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.FoodItems
{
    public class UpdateFoodItemsRequest
    {

        [FromBody] public FoodItemsModel UpdatedItem { get; set; } = default!;


    }
    public class UpdateFoodItems : EndpointBaseAsync
        .WithRequest<UpdateFoodItemsRequest>
        .WithActionResult<List<FoodItemsModel>>
    {

        private readonly IFoodItemsRepo _food;

        public UpdateFoodItems(IFoodItemsRepo food)
        {
            _food = food;
        }
        [HttpPut("food_items/{id:int}")]
        [SwaggerOperation(Summary = "Update a food item",
           Description = "Update a food item",
           OperationId = "FoodItems.Update",
           Tags = new[] { "FoodItemsEndpoint" })]
        public override async Task<ActionResult<List<FoodItemsModel>>> HandleAsync([FromMultiSource]UpdateFoodItemsRequest request, CancellationToken cancellationToken = default)
        {
            int id = int.Parse(HttpContext.Request.RouteValues["id"].ToString());
            var item = await _food.GetItem(id);
            if (item is null)
            {
                return null;
            }
            item.FirstOrDefault().FoodCategory = request.UpdatedItem.FoodCategory;
            item.FirstOrDefault().Price = request.UpdatedItem.Price;
            item.FirstOrDefault().ItemName = request.UpdatedItem.ItemName;


            await _food.UpdateItem(id, item.FirstOrDefault());

            return Ok("Updated");
        }
    }
}
