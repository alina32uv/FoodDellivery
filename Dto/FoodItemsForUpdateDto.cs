using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Dto
{
    public class FoodItemsForUpdateDto
    {
        public short Id { get; set; }
        [Required]
        [StringLength(256)]
        public string? ItemName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public short FoodCategory { get; set; }

    }
}
