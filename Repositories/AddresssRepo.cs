using Dapper;
using FoodDelivery.Data;
using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using System.Data;

namespace FoodDelivery.Repositories
{
    public class AddresssRepo: IAddressRepo
    {
        private readonly DapperContext ctx;
        public AddresssRepo(DapperContext ctx)
        {
            this.ctx = ctx;

        }

        public async Task<AddressModel> GetAddress(int id)
        {
            var query = "SELECT address_id AS Address_Id, house_number as HouseNumber, street_name as Street, city as City, postal_code as PostalCode " +
                 "FROM address WHERE address_id = @Id";

            using (var connection = ctx.CreateConnection())
            {
                var address = await connection.QuerySingleOrDefaultAsync<AddressModel>(query, new { id });
                return address;
            }
        }

        public async Task<IEnumerable<AddressModel>> GetAddresses()
        {
            string query = "select address_id as Id, house_number as House_number , street_name as Street, city as City, postal_code as Postal_code from address;";
            using (var connection = ctx.CreateConnection())
            {
                var address = await connection.QueryAsync<AddressModel>(query);
                return address;
            }
        }

        public async Task UpdateAddress(int id, AddressForUpdateDto address)
        {
           
                var query = " UPDATE address SET house_number = @HouseNumber, street_name = @Street, " +
                "city = @City, postal_code = @PostalCode  " +
                   " where address_id=@id; ";

                var parameters = new DynamicParameters();
                parameters.Add("Id", id, DbType.Int32);
                parameters.Add("HouseNumber", address.HouseNumber, DbType.Int32);
                parameters.Add("Street", address.Street, DbType.String);
            parameters.Add("City", address.City, DbType.String);
            parameters.Add("PostalCode", address.PostalCode, DbType.String);

            using (var connection = ctx.CreateConnection())
                {
                    await connection.ExecuteAsync(query, parameters);

                }
            
        }

        public async Task UpdateAddress(int id, AddressModel address)
        {
            var query = " UPDATE address SET house_number = @HouseNumber, street_name = @Street, " +
               "city = @City, postal_code = @PostalCode  " +
                  " where address_id=@id; ";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("HouseNumber", address.HouseNumber, DbType.Int32);
            parameters.Add("Street", address.Street, DbType.String);
            parameters.Add("City", address.City, DbType.String);
            parameters.Add("PostalCode", address.PostalCode, DbType.String);

            using (var connection = ctx.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);

            }
        }
    }
}
