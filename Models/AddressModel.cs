namespace FoodDelivery.Models
{
    public class AddressModel
    {
        public short Address_Id { get; set; }
        public short HouseNumber { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
    }
}
