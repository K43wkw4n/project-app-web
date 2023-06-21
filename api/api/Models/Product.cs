namespace api.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; } 
        public string Image { get; set; } 
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }  
        public string Brand { get; set; } 
        public string Source { get; set; } 
        public List<ProductImage> ProductImages { get; set; }
    }
} 
