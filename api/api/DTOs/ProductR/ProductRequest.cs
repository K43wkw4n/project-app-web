using System.ComponentModel.DataAnnotations;

namespace api.DTOs.ProductR
{
    public class ProductRequest
    {
        public int? ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile? Image { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Size { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Source { get; set; }
        public IFormFileCollection? FormFiles { get; set; }
    }
}
