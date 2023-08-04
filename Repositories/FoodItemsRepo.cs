using Dapper;
using FoodDelivery.Data;
using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using System.Data;

namespace FoodDelivery.Repositories
{
    public class FoodItemsRepo: IFoodItemsRepo
    {
        private readonly DapperContext ctx;
        public FoodItemsRepo(DapperContext ctx)
        {
            this.ctx = ctx;

        }

        public async Task<IEnumerable<FoodItemsModel>> GetItem(int id)
        {
           
            string query = "select food_item_id as Id, item_name as ItemName, item_price as Price, food_category_id as FoodCategory " +
               "from food_item " +
               "WHERE food_category_id = @Id;";

            using (var connection = ctx.CreateConnection())
            {
                var item = await connection.QueryAsync<FoodItemsModel>(query, new { id });
                return item;
            }
        }

        public async Task<IEnumerable<FoodItemsModel>> GetItemName(string name)
        {
            string query = "select food_item_id as Id, item_name as ItemName, item_price as Price, food_category_id as FoodCategory " +
               "from food_item " +
               "WHERE item_name like '%' + @name + '%';";

            using (var connection = ctx.CreateConnection())
            {
                var item = await connection.QueryAsync<FoodItemsModel>(query, new { name });
                return item;
            }
        }

        public async  Task<IEnumerable<FoodItemsModel>> GetItems()
        {
            string query = "select food_item_id as Id, item_name as ItemName, item_price as Price, food_category_id as FoodCategory " +
                "from food_item ;";
            using (var connection = ctx.CreateConnection())
            {
                var items = await connection.QueryAsync<FoodItemsModel>(query);
                return items;
            }
        }

        public async Task UpdateItem(int id, FoodItemsForUpdateDto item)
        {
            var query = " UPDATE food_item SET item_name = @Name, item_price = @Price, food_category_id = @Category  " +
                "where food_item_id=@id; ";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", item.ItemName, DbType.String);
            parameters.Add("Price", item.Price, DbType.Decimal);
            parameters.Add("Category", item.FoodCategory, DbType.Int32);


            using (var connection = ctx.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);

            }
        }

        public async Task UpdateItem(int id, FoodItemsModel? item)
        {
            var query = " UPDATE food_item SET item_name = @Name, item_price = @Price, food_category_id = @Category  " +
                "where food_item_id=@id; ";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", item.ItemName, DbType.String);
            parameters.Add("Price", item.Price, DbType.Decimal);
            parameters.Add("Category", item.FoodCategory, DbType.Int32);


            using (var connection = ctx.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);

            }
        }
    }
}
