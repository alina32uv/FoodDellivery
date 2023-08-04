using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using FoodDelivery.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/food_category")]
    [ApiController]
    public class FoodCategoryController : ControllerBase
    {
        private readonly IFoodCategoryRepo categoryRepo;

        public FoodCategoryController(IFoodCategoryRepo categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }


        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await categoryRepo.GetCategories();
                return Ok(categories);



            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }



        }


        [HttpGet("{id}", Name = "CategoryById")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await categoryRepo.GetCategory(id);
            if (category is null)
                return NotFound();
            return Ok(category);


        }
        [Authorize]
        [HttpPost("add/{id}")]
        public async Task<ActionResult> CreateCategory(int id, [FromBody] FoodCategoryForCreationDto category)
        {
            var createdCategory = await categoryRepo.CreateCategory(id, category);
            return CreatedAtRoute("CategoryById", new { id = createdCategory.Id }, createdCategory);

        }


       
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] FoodCategoryForUpdateDto category)
        {
            var dbCategory = await categoryRepo.GetCategory(id);
            if (dbCategory is null)
                return NotFound();
            await categoryRepo.UpdateCategory(id, category);
            return NoContent();
        } 
    }
}
