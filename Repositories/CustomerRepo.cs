using Dapper;
using FoodDelivery.Data;
using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using System.Data;

namespace FoodDelivery.Repositories 
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly DapperContext ctx;
        public CustomerRepo(DapperContext ctx)
        {
            this.ctx = ctx;

        }

        public async Task<CustomersModel> CreateCustomer(CustomersForCreationDto customer)
        {
            var query = "INSERT INTO customer (first_name, last_name) VALUES ( @FName, @LName) " +
               "SELECT CAST(SCOPE_IDENTITY() AS int)";

            var parameters = new DynamicParameters();
            parameters.Add("FName", customer.FName, DbType.String);
            parameters.Add("LName", customer.LName, DbType.String);


            using (var connection = ctx.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<short>(query, parameters);
                var createdCustomer = new CustomersModel
                {
                    Id = id,
                    FName = customer.FName,
                    LName = customer.LName,

                };
                return createdCustomer;

            }
        }

        public async Task<CustomersModel> GetCustomer(int id)
        {
            var query = "SELECT customer_id AS Id, first_name AS FName, last_name as LName " +
                "FROM customer WHERE customer_id = @Id";

            using (var connection = ctx.CreateConnection())
            {
                var customer = await connection.QuerySingleOrDefaultAsync<CustomersModel>(query, new { id });
                return customer;
            }
        }

        public async Task<IEnumerable<CustomersModel>> GetCustomers()
        {
            string query = "select customer_id as Id, first_name as FName , last_name as LName from customer;";
            using (var connection = ctx.CreateConnection())
            {
                var customers = await connection.QueryAsync<CustomersModel>(query);
                return customers;
            }
        }

        public async Task UpdateCustomer(int id, CustomerForUpdateDto customer)
        {
            var query = " UPDATE customer SET first_name = @FName, last_name = @LName" +
               " where customer_id=@id; ";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("FName", customer.FName, DbType.String);
            parameters.Add("LName", customer.LName, DbType.String);


            using (var connection = ctx.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);

            }
        }

        public async Task UpdateCustomer(int id, CustomersModel customer)
        {
            var query = " UPDATE customer SET first_name = @FName, last_name = @LName" +
               " where customer_id=@id; ";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("FName", customer.FName, DbType.String);
            parameters.Add("LName", customer.LName, DbType.String);


            using (var connection = ctx.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);

            }
        }
    }
}
