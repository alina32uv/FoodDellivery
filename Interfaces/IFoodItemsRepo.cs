using FoodDelivery.Dto;
using FoodDelivery.Models;

namespace FoodDelivery.Interfaces
{
    public interface IFoodItemsRepo
    {
        public Task<IEnumerable<FoodItemsModel>> GetItems();
        public Task<IEnumerable<FoodItemsModel>> GetItem(int id);
        public Task<IEnumerable<FoodItemsModel>> GetItemName(string name);

        public Task UpdateItem(int id, FoodItemsForUpdateDto item);
        public Task UpdateItem(int id, FoodItemsModel? item);
    }
}
