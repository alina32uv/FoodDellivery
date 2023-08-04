using Dapper;
using FoodDelivery.Data;
using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using System.Data;

namespace FoodDelivery.Repositories
{
    public class FoodOrdersRepo : IFoodOrdersRepo
    {
        private readonly DapperContext ctx;
        public FoodOrdersRepo(DapperContext ctx)
        {
            this.ctx = ctx;

        }

        public async Task<FoodOrdersModel> CreateOrder(FoodOrdersForCreationDto order)
        {
            var query = "INSERT INTO food_order (address_id, user_id, order_status_id, restaurant_id, " +
                "delivery_fee, total_amount, order_date, req_delivery_date, customer_id) VALUES ( @Address, @User,  " +
                "@OrderStatus, @Restaurant, @DeliveryFee, @TotalAmount, @OrderDate, @ReqDelivery, @Customer) " +
                 "SELECT CAST(SCOPE_IDENTITY() AS int)";

            var parameters = new DynamicParameters();
            parameters.Add("Address", order.Address_Id, DbType.Int32);
            parameters.Add("User", order.User_Id, DbType.Int32);
            parameters.Add("OrderStatus", order.OrderStatus_Id, DbType.Int32);
            parameters.Add("Restaurant", order.Restaurant_Id, DbType.Int32);
            parameters.Add("DeliveryFee", order.DeliveryFee, DbType.Decimal);
            parameters.Add("TotalAmount", order.TotalAmount, DbType.Decimal);
            parameters.Add("OrderDate", order.OrderDate, DbType.DateTime);
            parameters.Add("ReqDelivery", order.ReqDeliveryDate, DbType.DateTime);
            parameters.Add("Customer", order.Customer_Id, DbType.Int32);


            using (var connection = ctx.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<short>(query, parameters);
                var createdOrder = new FoodOrdersModel
                {
                    Id = id,
                    Address_Id = order.Address_Id,
                    User_Id = order.User_Id,
                    OrderStatus_Id= order.OrderStatus_Id,
                    Restaurant_Id = order.Restaurant_Id,
                    DeliveryFee = order.DeliveryFee,
                    TotalAmount = order.TotalAmount,
                    OrderDate = order.OrderDate,
                    ReqDeliveryDate = order.ReqDeliveryDate,
                    Customer_Id  = order.Customer_Id

                };
                return createdOrder;

            }
        }

        public async Task DeleteOrder(int id)
        {
            var query = "delete from food_order where food_order_id = @Id;  ";
            using (var coonection = ctx.CreateConnection())
            {
                await coonection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<FoodOrdersModel> GetOrder(int id)
        {
            var query = "select food_order_id as Id, address_id as Address_Id, user_id as User_Id, customer_id as Customer_Id, " +
                "order_status_id as OrderStatus_Id, restaurant_id as Restaurant_Id, delivery_fee as DeliveryFee, " +
                "total_amount as TotalAmount, order_date as OrderDate, req_delivery_date as ReqDeliveryDate from " +
                "food_order " +
                "where food_order_id = @id;";

            using (var connection = ctx.CreateConnection())
            {
                var order = await connection.QuerySingleOrDefaultAsync<FoodOrdersModel>(query, new { id });
                return order;
            }
        }

        public async Task<IEnumerable<FoodOrdersModel>> GetOrders()
        {
           

            string query = "select food_order_id as Id, address_id as Address_Id, user_id as User_Id, customer_id as Customer_Id, " +
                "order_status_id as OrderStatus_Id, restaurant_id as Restaurant_Id, delivery_fee as DeliveryFee, " +
                "total_amount as TotalAmount, order_date as OrderDate, req_delivery_date as ReqDeliveryDate from " +
                "food_order ;";
            using (var connection = ctx.CreateConnection())
            {
                var orders = await connection.QueryAsync<FoodOrdersModel>(query);
                return orders;
            }
        }

        public async Task UpdateOrder(int id, FoodOrdersForUpdateDto order)
        {
            var query = " UPDATE food_order SET user_id = @User " +
                "where food_order_id=@id; ";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("User", order.User_Id, DbType.Int32);


            using (var connection = ctx.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);

            }
        }

        public async Task UpdateOrder(int id, FoodOrdersModel order)
        {
            var query = " UPDATE food_order SET user_id = @User " +
               "where food_order_id=@id; ";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("User", order.User_Id, DbType.Int32);


            using (var connection = ctx.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);

            }
        }
    }
}
