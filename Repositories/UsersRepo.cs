using Dapper;
using FoodDelivery.Data;
using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using System.Data;

namespace FoodDelivery.Repositories
{
    public class UsersRepo: IUsersRepo
    {
        private readonly DapperContext ctx;
        public UsersRepo(DapperContext ctx)
        {
            this.ctx = ctx;

        }

        public async Task<UsersModel> CreateUser(UsersForCreationDto user)
        {
            var query = "INSERT INTO users (first_name, last_name, email, password)" +
                " VALUES ( @FName, @LName, @Email,  @Password) " +
                "SELECT CAST(SCOPE_IDENTITY() AS int)";

            var parameters = new DynamicParameters();
            parameters.Add("FName", user.FName, DbType.String);
            parameters.Add("LName", user.LName, DbType.String);
            parameters.Add("Email", user.Email, DbType.String);
            parameters.Add("Password", user.Password, DbType.String);


            using (var connection = ctx.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<short>(query, parameters);
                var createdUser = new UsersModel
                {
                    Id = id,
                    FName = user.FName,
                   LName = user.LName,
                    Email = user.Email,
                    Password = user.Password 

                };
                return createdUser;

            }
        }

        public async Task<UsersModel> GetUser(int id)
        {
            var query = "select ur.user_id as Id, first_name as FName, last_name as LName, email as Email, password as Password, role_name as Role  " +
                 "from user_role ur " +
                "join users u on ur.user_id=u.user_id" +
                " join roles r on ur.role_id=r.role_id "+
               
                "WHERE u.user_id = @Id";

            using (var connection = ctx.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<UsersModel>(query, new { id });
                return user;
            }
        }

        public async  Task<IEnumerable<UsersModel>> GetUsers()
        {
            string query = "select ur.user_id as Id, first_name as FName, last_name as LName, email as Email, password as Password, role_name as Role  " +
                "from user_role ur " +
                "join users u on ur.user_id=u.user_id" +
                " join roles r on ur.role_id=r.role_id ";
            using (var connection = ctx.CreateConnection())
            {
                var users = await connection.QueryAsync<UsersModel>(query);
                return users;
            }
        }

        public async Task<UsersModel> GetUser(string Fname, string Password)
        {
            var query = "select ur.user_id as Id, first_name as FName, last_name as LName, email as Email, password as Password, role_name as Role  " +
                "from user_role ur " +
                "join users u on ur.user_id=u.user_id" +
                " join roles r on ur.role_id=r.role_id " +
                "WHERE first_name = @Fname AND password = @Password";
           


            using (var connection = ctx.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<UsersModel>(query, new { Fname, Password });
                return user;
            }
        }
    }
}
