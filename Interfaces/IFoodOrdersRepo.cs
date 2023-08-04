using FoodDelivery.Dto;
using FoodDelivery.Models;

namespace FoodDelivery.Interfaces
{
    public interface IFoodOrdersRepo
    {
        public Task<IEnumerable<FoodOrdersModel>> GetOrders();
        public Task<FoodOrdersModel> GetOrder(int id);
        public Task<FoodOrdersModel> CreateOrder(FoodOrdersForCreationDto order);
        public Task UpdateOrder(int id, FoodOrdersForUpdateDto order);
        public Task UpdateOrder(int id, FoodOrdersModel order);
        public Task DeleteOrder(int id);
    }
}
