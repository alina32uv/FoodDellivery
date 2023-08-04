namespace FoodDelivery.Models
{
    public class FoodItemsModel
    {
        public short Id { get; set; }
        public string? ItemName { get; set; }
        public decimal Price { get; set; }
        public short FoodCategory { get; set; }

    }
}
