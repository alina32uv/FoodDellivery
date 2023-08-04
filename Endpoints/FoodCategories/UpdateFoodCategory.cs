using Ardalis.ApiEndpoints;
using Azure;
using FoodDelivery.Endpoints.FoodOrders;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.FoodCategories
{
    public class UpdateFoodCategoryRequest
    {

        [FromBody] public FoodCategoryModel UpdatedCategory { get; set; } = default!;


    }
    public class UpdateFoodCategory : EndpointBaseAsync
        .WithRequest<UpdateFoodCategoryRequest>
        .WithActionResult<FoodCategoryModel>
    {
        private readonly IFoodCategoryRepo _food;

        public UpdateFoodCategory(IFoodCategoryRepo food)
        {
            _food = food;
        }

        [HttpPut("food_category/{id:int}")]
        [SwaggerOperation(Summary = "Update food category ",
            Description = "Update food category ",
            OperationId = "FoodCategory.Update",
            Tags = new[] { "FoodCategoryEndpoint" })]
        public override async  Task<ActionResult<FoodCategoryModel>> HandleAsync(UpdateFoodCategoryRequest request, CancellationToken cancellationToken = default)
        {

            int id = int.Parse(HttpContext.Request.RouteValues["id"].ToString());
            var category = await _food.GetCategory(id);
            if (category is null)
            {
                return null;
            }
            category.FirstOrDefault().Category = request.UpdatedCategory.Category;


            await _food.UpdateCategory(id, category.FirstOrDefault());

            return Ok("Updated");
        }
    }
}
