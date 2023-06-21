using api.DTOs;
using api.DTOs.CouponR;
using api.Service.CouponS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCoupon()
            => Ok(await _couponService.GetCouponAsync());

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAndUpdateCoupon(CouponRequest couponRequest)
            => Ok(await _couponService.CreateAndUpdateCouponAsync(couponRequest));

        [HttpDelete("[action]")]
        public async Task<IActionResult> Remove(string id)
            => Ok(await _couponService.RemoveAsync(id));

         
    }
}
