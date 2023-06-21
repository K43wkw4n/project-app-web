using System.Text.Json.Serialization;

namespace api.Models
{
    public class Favorite
    {
        public int ID { get; set; }
        public int UserId { get; set; } //FK
        [JsonIgnore]
        public User User { get; set; }

        public int ProductId { get; set; } //FK 
        public Product Product { get; set; }
         
    }
}
