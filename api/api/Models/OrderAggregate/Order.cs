using System.Text.Json.Serialization;

namespace api.Models.OrderAggregate
{
    public class Order
    {
        public string ID { get; set; }
        public string PaymentImage { get; set; }
        public DateTime DateTimeConfirm { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        public string? CouponId { get; set; } = string.Empty; //FK
        [JsonIgnore]
        public Coupon Coupon { get; set; }

        public int ShopCartId { get; set; }
        [JsonIgnore]
        public ShopCart ShopCart { get; set; }

    }
}
