namespace api.DTOs.ProductR
{
    public class ProductResponse
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
        public List<string> ImageUrls { get; set; }

        public static ProductResponse FromProduct(Product product)
        {
            return new ProductResponse
            {
                ID = product.ID,
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                Price = product.Price,
                Quantity = product.Quantity,
                Size = product.Size,
                Brand = product.Brand,
                Source = product.Source,
                ImageUrls = product.ProductImages.Select(x => x.Image).ToList()
            };
        }

    }
}
