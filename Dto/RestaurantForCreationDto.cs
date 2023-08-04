using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Dto
{
    public class RestaurantForCreationDto
    {
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        [Required]
        
        public short Address { get; set; }

    }
}
