namespace api.DTOs.CouponR
{
    public class CouponResponse
    {
        public string ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DisCount { get; set; }
        public int Quantity { get; set; }
        public DateTime Expire { get; set; }

        static public CouponResponse FromCupon(Coupon coupon)
        {
            return new CouponResponse
            {
                ID = coupon.ID,
                Name = coupon.Name,
                DisCount = coupon.DisCount,
                Quantity = coupon.Quantity,
                Expire = coupon.Expire,
            };
        }

    }
}
