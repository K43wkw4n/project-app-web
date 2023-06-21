using System.ComponentModel.DataAnnotations;

namespace api.DTOs.CouponR
{
    public class CouponRequest
    {
        public string? ID { get; set; }
        [Required]
       // [MaxLength(100, ErrorMessage = "no more than {1} chars")]
        public string Name { get; set; } 
        [Required]
       // [Range(0, 100, ErrorMessage = "between {1}-{2}")]
        public int DisCount { get; set; }
        [Required]
       // [Range(0, 1000, ErrorMessage = "between {1}-{2}")]
        public int Quantity { get; set; }
        [Required]
        public DateTime Expire { get; set; }
    }
}
