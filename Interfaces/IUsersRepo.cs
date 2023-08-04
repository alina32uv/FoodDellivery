using FoodDelivery.Dto;
using FoodDelivery.Models;

namespace FoodDelivery.Interfaces
{
    public interface IUsersRepo
    {
        public Task<IEnumerable<UsersModel>> GetUsers();
        public Task<UsersModel> CreateUser(UsersForCreationDto user);
        public Task<UsersModel> GetUser(int id);
        public Task<UsersModel> GetUser(string Fname, string Password );

    }
}
