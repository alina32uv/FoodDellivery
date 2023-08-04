using Ardalis.ApiEndpoints;
using Azure;
using FoodDelivery.Dto;
using FoodDelivery.Endpoints.FoodOrders;
using FoodDelivery.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.FoodCategories
{

    public class CreateFoodCategoryRequest
    {

        [FromBody] public FoodCategoryForCreationDto CreatedCategory { get; set; } = default!;
    }


    public class CreateFoodCategory : EndpointBaseAsync
        .WithRequest<CreateFoodCategoryRequest>
        .WithActionResult<FoodCategoryForCreationDto>
    {

        private readonly IFoodCategoryRepo _food;

        public CreateFoodCategory(IFoodCategoryRepo food)
        {
            _food = food;
        }

        [HttpPost("food_category/{id:int}")]
        [SwaggerOperation(Summary = "Create food category ",
            Description = "Create food category",
            OperationId = "FoodCategory.Create",
            Tags = new[] { "FoodCategoryEndpoint" })]

        public override async Task<ActionResult<FoodCategoryForCreationDto>> HandleAsync(CreateFoodCategoryRequest request, CancellationToken cancellationToken = default)
        {
            int restaurantId = int.Parse(HttpContext.Request.RouteValues["id"].ToString());

            var createdCategory = await _food.CreateCategory(restaurantId, request.CreatedCategory);
            return CreatedAtRoute("FoodCategoryById", new { id = createdCategory.Id }, createdCategory);
        }
    }
}
