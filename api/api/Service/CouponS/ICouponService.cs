using api.DTOs.CouponR;

namespace api.Service.CouponS
{
    public interface ICouponService
    {
        Task<List<Coupon>> GetCouponAsync();
        Task<Object> CreateAndUpdateCouponAsync(CouponRequest couponRequest);
        Task<Object> RemoveAsync(string id);
    }
}
