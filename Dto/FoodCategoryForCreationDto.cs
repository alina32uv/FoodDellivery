using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Dto
{
    public class FoodCategoryForCreationDto
    {
        public short Id { get; set; }
        [Required]
        [StringLength(50)]
        public string? Category { get; set; }
        [Required]
        [StringLength(50)]
        public string? Restaurant { get; set; }
    }
}
