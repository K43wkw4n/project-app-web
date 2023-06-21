using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.DTOs
{
    public class AddressDto
    {
        public int ID { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public string SubDistrict { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string HouseNumber { get; set; }
        [Required]
        public int UserId { get; set; } //FK
    }
}
