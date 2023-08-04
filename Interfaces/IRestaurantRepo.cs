using FoodDelivery.Dto;
using FoodDelivery.Models;

namespace FoodDelivery.Interfaces
{
    public interface IRestaurantRepo
    {
       public  Task<IEnumerable<RestaurantModel>> GetRestaurants();
        public Task<RestaurantModel> GetRestaurant(int id);
        public Task<RestaurantModel> CreateRestaurant(RestaurantForCreationDto restaurant);
        public Task<IEnumerable<Menu>> GetMenu(int id);
        public Task UpdateRestaurant(int id, RestaurantsForUpdateDto restaurant);
        Task UpdateRestaurant(int id, RestaurantModel restaurant);
        public Task DeleteRestaurant(int id);

    }
}
