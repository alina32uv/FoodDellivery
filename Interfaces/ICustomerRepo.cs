using FoodDelivery.Dto;
using FoodDelivery.Models;

namespace FoodDelivery.Interfaces
{
    public interface ICustomerRepo
    {
        public Task<IEnumerable<CustomersModel>> GetCustomers();
        public Task<CustomersModel> GetCustomer(int id);
        public Task<CustomersModel> CreateCustomer(CustomersForCreationDto customer);
        public Task UpdateCustomer(int id, CustomerForUpdateDto customer);
        public Task UpdateCustomer(int id, CustomersModel customer);
    }
}
