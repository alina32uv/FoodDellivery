using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Dto
{
    public class AddressForUpdateDto
    {

        public short Address_Id { get; set; }
        public short HouseNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string? Street { get; set; }
        [Required]
        [StringLength(50)]
        public string? City { get; set; }
        [Required]
        [StringLength(10)]
        public string? PostalCode { get; set; }
    }
}
