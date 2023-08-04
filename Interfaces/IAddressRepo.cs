using FoodDelivery.Dto;
using FoodDelivery.Models;

namespace FoodDelivery.Interfaces
{
    public interface IAddressRepo
    {
        public Task<AddressModel> GetAddress(int id);
       public Task<IEnumerable<AddressModel>> GetAddresses();
        public Task UpdateAddress(int id, AddressForUpdateDto address);
       public  Task UpdateAddress(int id, AddressModel address);
    }
}
