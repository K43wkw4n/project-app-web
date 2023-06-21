using System.ComponentModel.DataAnnotations;

namespace api.DTOs
{
    public class ProductDto
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; } 
        [Required]
        public string Description { get; set; } 
        [Required]
        public string Image { get; set; } 
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
        //public List<string> ImageUrls { get; set; }
    }
}
