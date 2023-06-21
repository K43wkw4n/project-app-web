namespace api.Models
{
    public class Coupon
    {
        public string ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DisCount { get; set; }
        public int Quantity { get; set; }
        public DateTime Expire { get; set; }
    }
}
