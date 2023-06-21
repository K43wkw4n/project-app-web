using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api.Models
{
    [Table("ShopCartItems")]
    public class ShopCartItem
    {
        public int ID { get; set; }
        public int Quantity { get; set; }

        public int ProductId { get; set; } // FK 
        public Product Product { get; set; }

        public int ShopCartId { get; set; } // FK 
        [JsonIgnore]
        public ShopCart ShopCart { get; set; }
    }
}
