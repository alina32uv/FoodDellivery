using Ardalis.ApiEndpoints;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.FoodCategories
{
    public class GetAllFoodCategories : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<List<FoodCategoryModel>>
    {
       private readonly IFoodCategoryRepo _food;

        public GetAllFoodCategories(IFoodCategoryRepo food)
        {
            _food = food;
        }
        [HttpGet("food_category")]
        [SwaggerOperation(Summary = "Get all food categories",
            Description = "Get all food categoires ",
            OperationId = "FoodCategory.GetAll",
            Tags = new[] { "FoodCategoryEndpoint" })]

        public override async Task<ActionResult<List<FoodCategoryModel>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var food = await _food.GetCategories();
            return Ok(food);
        }
    }
}
