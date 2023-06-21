using System.Text.Json.Serialization;

namespace api.Models
{
    public class Address
    {
        public int ID { get; set; }
        public string Province { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string SubDistrict { get; set; } = string.Empty;
        public string PostCode { get; set; } = string.Empty;
        public string HouseNumber { get; set; } = string.Empty;

        public int UserId { get; set; } //FK
        [JsonIgnore]
        public User User { get; set; }

    }
}
