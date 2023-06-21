using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api.Models
{
    public class ShopCart
    {
        public int ID { get; set; }
        public DateTime createDate { get; set; }
        public int UserId { get; set; } // FK 
        [JsonIgnore]
        public User User { get; set; }
        public List<ShopCartItem> Items { get; set; } = new();

        public void AddItem(Product product, int quantity)
        {
            if (Items.All(item => item.ProductId != product.ID))
            {
                Items.Add(new ShopCartItem { Product = product, Quantity = quantity });
            }
           
            var existingItem = Items.FirstOrDefault(item => item.ProductId == product.ID);
            if (existingItem != null) existingItem.Quantity += quantity;
        }

        public void RemoveItem(int productId, int quantity)
        {
            var item = Items.FirstOrDefault(item => item.ProductId == productId);
            if (item == null) return;
            item.Quantity -= quantity;
            if (item.Quantity <= 0) Items.Remove(item);
        }
    }
}
