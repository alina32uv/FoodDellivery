using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Dto
{
    public class CustomerForUpdateDto
    {
        [Required]
        [StringLength(256)]
        public string? FName { get; set; }
        [Required]
        [StringLength(256)]
        public string? LName { get; set; }
    }
}
