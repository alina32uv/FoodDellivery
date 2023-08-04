using Ardalis.ApiEndpoints;
using Azure;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.FoodCategories
{
    public class GetFoodCategoryById : EndpointBaseAsync
       .WithoutRequest
        .WithResult<List<FoodCategoryModel>>
    {
        private readonly IFoodCategoryRepo _food;

        public GetFoodCategoryById(IFoodCategoryRepo food)
        {
            _food = food;
        }

        [HttpGet("food_category/{id:int}", Name = "FoodCategoryById")]
        [SwaggerOperation(Summary = "Get food category by restaurant's addres id",
            Description = "Get food category by by restaurant's addres id ",
            OperationId = "FoodCategory.GetAll",
            Tags = new[] { "FoodCategoryEndpoint" })]
        public override async  Task<List<FoodCategoryModel>> HandleAsync(CancellationToken cancellationToken = default)
        {
            int id = int.Parse(HttpContext.Request.RouteValues["id"].ToString());

            var foodc = await _food.GetCategory(id);
            if (foodc is null)
                return null;
            return (List<FoodCategoryModel>)foodc;
        }
    }
}
