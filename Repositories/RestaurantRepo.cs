using Dapper;
using FoodDelivery.Data;
using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using System.Data;

namespace FoodDelivery.Repositories
{
    public class RestaurantRepo : IRestaurantRepo
    {
        private readonly DapperContext ctx;
        public RestaurantRepo(DapperContext ctx)
        {
            this.ctx = ctx;

        }

       

        public async Task<IEnumerable<RestaurantModel>> GetRestaurants()
        {
            string query = "select restaurant_id as Id, restaurant_name as Name, a.address_id as AddressId from restaurants r " +
                " join address a on r.address_id = r.address_id ;";
            using (var connection = ctx.CreateConnection())
            {
                var restaurants = await connection.QueryAsync<RestaurantModel>(query);
                return restaurants;
            }
        }

        public async Task<RestaurantModel> GetRestaurant(int id) 
        {
            var query = "SELECT r.restaurant_id AS Id, r.restaurant_name AS Name, a.address_id AS AddressId " +
                "FROM restaurants r " +
                "JOIN address a ON r.address_id = a.address_id " +
                "WHERE r.restaurant_id = @Id";

            using (var connection = ctx.CreateConnection())
            {
                var restaurant = await connection.QuerySingleOrDefaultAsync<RestaurantModel>(query, new { id});
                return restaurant;
            }
        }

        public async Task<RestaurantModel> CreateRestaurant(RestaurantForCreationDto restaurant)
        {
            var query = "INSERT INTO restaurants (restaurant_name, address_id) VALUES ( @Name, @Address) " +
                "SELECT CAST(SCOPE_IDENTITY() AS int)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", restaurant.Name, DbType.String);
            parameters.Add("Address", restaurant.Address, DbType.Int32);


            using (var connection = ctx.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<short>(query, parameters);
                var createdRestaurant = new RestaurantModel
                {
                    Id = id,
                    Name = restaurant.Name,
                    AddressId = restaurant.Address,

                };
                return createdRestaurant;

            }
        

        }

        public async Task<IEnumerable<Menu>> GetMenu(int id)
        {
            
            var query = "select f.category_name as FoodCategory, r.restaurant_name as RName, i.item_name as FoodItem, " +
                "i.item_price as ItemPrice from " +
                 "food_category f  join restaurants r on f.restaurant_id = r.restaurant_id " +
                 "join food_item i on f.food_category_id = i.food_item_id " +
                 "where f.restaurant_id=@id";

         

            using (var connection = ctx.CreateConnection())
            {
                var menu = await connection.QueryAsync<Menu>(query, new { id });
                return menu;
            }
        }

        public async Task UpdateRestaurant(int id, RestaurantsForUpdateDto restaurant )
        {
            var query = " UPDATE restaurants SET restaurant_name = @Name " +
                "where restaurant_id=@id; ";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", restaurant.Name , DbType.String);


            using (var connection = ctx.CreateConnection())
            {
                 await connection.ExecuteAsync(query, parameters);
                 
            }
        }

        public async Task UpdateRestaurant(int id, RestaurantModel restaurant)
        {
            var query = " UPDATE restaurants SET restaurant_name = @Name " +
                 "where restaurant_id=@id; ";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", restaurant.Name, DbType.String);


            using (var connection = ctx.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);

            }
        }

        public async Task DeleteRestaurant(int id)
        {
            var query = "delete from restaurants where restaurant_id = @Id;  ";
            using (var coonection = ctx.CreateConnection())
            {
                await coonection.ExecuteAsync(query, new { id });
            }

        }
    }
}
