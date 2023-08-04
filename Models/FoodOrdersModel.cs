namespace FoodDelivery.Models
{
    public class FoodOrdersModel
    {
        public short Id { get; set; }
        public short Address_Id { get; set; }
        public short User_Id { get; set; }
        public short Customer_Id { get; set; }
        public short OrderStatus_Id { get; set; }
        public short Restaurant_Id { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ReqDeliveryDate { get; set; }



    }
}
