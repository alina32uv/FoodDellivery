using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Dto
{
    public class FoodOrdersForCreationDto
    {
        //public short Id { get; set; }
        [Required]
        public short Address_Id { get; set; }
        [Required]
        public short User_Id { get; set; }
        [Required]
        public short Customer_Id { get; set; }
        [Required]
        public short OrderStatus_Id { get; set; }
        [Required]
        public short Restaurant_Id { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "The delivery fee must be a positive number")]
        public decimal DeliveryFee { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "The delivery fee must be a positive number")]
        public decimal TotalAmount { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ReqDeliveryDate { get; set; }
    }
}
