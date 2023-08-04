using FoodDelivery.Dto;
using FoodDelivery.Models;


namespace FoodDelivery.Interfaces
{
    public interface IFoodCategoryRepo
    {
        public Task<IEnumerable<FoodCategoryModel>> GetCategories();
        public Task<IEnumerable<FoodCategoryModel>> GetCategory(int id);
        public Task<FoodCategoryModel> CreateCategory(int id, FoodCategoryForCreationDto category);

        public Task UpdateCategory(int id, FoodCategoryForUpdateDto category);
        public Task UpdateCategory(int id, FoodCategoryModel category);

    }
}
