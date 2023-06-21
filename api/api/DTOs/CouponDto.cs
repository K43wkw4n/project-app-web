using System.ComponentModel.DataAnnotations;

namespace api.DTOs
{
    public class CouponDto
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; } 
        [Required]
        public int DisCount { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public DateTime Expire { get; set; }
    }
}
