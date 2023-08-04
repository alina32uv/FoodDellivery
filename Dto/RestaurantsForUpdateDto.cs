using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Dto
{
    public class RestaurantsForUpdateDto
    {
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
            public short Address { get; set; }

        
    }
}
