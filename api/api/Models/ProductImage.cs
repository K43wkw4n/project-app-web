using System.Text.Json.Serialization;

namespace api.Models
{
    public class ProductImage
    {
        public int ID { get; set; }
        public string Image { get; set; }
        
        public int ProductId { get; set; } //FK
        [JsonIgnore]
        public Product Product { get; set; } = new();

    }
}
