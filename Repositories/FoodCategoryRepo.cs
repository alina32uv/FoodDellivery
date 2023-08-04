using Dapper;
using FoodDelivery.Data;
using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using System.Data;

namespace FoodDelivery.Repositories
{
    public class FoodCategoryRepo : IFoodCategoryRepo
    {
        private readonly DapperContext ctx;
        public FoodCategoryRepo(DapperContext ctx)
        {
            this.ctx = ctx;

        }

        public async Task<FoodCategoryModel> CreateCategory(int restaurantId, FoodCategoryForCreationDto category)
        {
            var query = "INSERT INTO food_category (category_name, restaurant_id) VALUES ( @Name, @Restaurant)  " +
               "SELECT CAST(SCOPE_IDENTITY() AS int)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", category.Category, DbType.String);
            parameters.Add("Restaurant", restaurantId, DbType.Int32);



            using (var connection = ctx.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<short>(query, parameters);
                var createdCategory = new FoodCategoryModel
                {
                    Id = id,
                    Category = category.Category,
                    Restaurant = restaurantId
                   

                };
                return createdCategory;

            }
        }

       

        public async Task<IEnumerable<FoodCategoryModel>> GetCategories()
        {
            string query = "select food_category_id as Id, category_name as Category, restaurant_id as Restaurant from food_category " +
                 ";";
            using (var connection = ctx.CreateConnection())
            {
                var categories = await connection.QueryAsync<FoodCategoryModel>(query);
                return categories;
            }
        }

        public async Task<IEnumerable<FoodCategoryModel>> GetCategory(int id)
        {
            var query = "SELECT food_category_id as Id,  category_name as Category, c.restaurant_id as Restaurant  " +
                "FROM food_category c " +
                "join restaurants r on c.restaurant_id= r.restaurant_id  " +
                " WHERE address_id = @Id";

            using (var connection = ctx.CreateConnection())
            {
                var category = await connection.QueryAsync<FoodCategoryModel>(query, new { id });
                return category;
            }
        }

        public async Task UpdateCategory(int id, FoodCategoryForUpdateDto category)
        {
            var query = " UPDATE food_category SET category_name = @Name " +
                "where food_category_id=@id; ";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", category.Category, DbType.String);


            using (var connection = ctx.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);

            }
        }

        public async Task UpdateCategory(int id, FoodCategoryModel category)
        {
            var query = " UPDATE food_category SET category_name = @Name " +
                "where food_category_id=@id; ";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", category.Category, DbType.String);


            using (var connection = ctx.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);

            }
        }
    }
}
