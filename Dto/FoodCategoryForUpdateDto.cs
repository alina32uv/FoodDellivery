using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Dto
{
    public class FoodCategoryForUpdateDto
    {
        public short Id { get; set; }
        [Required]
        [StringLength(50)]
        public string? Category { get; set; }
        public int Restaurant { get; set; }
    }
}
