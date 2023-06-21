using api.Data; 
using api.DTOs.CouponR; 
using AutoMapper; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Service.CouponS
{
    public class CouponService : ControllerBase, ICouponService
    {
        private string GenerateID() => Guid.NewGuid().ToString("N");
        private readonly Context _context;
        private readonly IMapper _mapper;

        public CouponService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Coupon>> GetCouponAsync()
        {
            return await _context.Coupons.ToListAsync();
        }

        public async Task<object> CreateAndUpdateCouponAsync(CouponRequest couponRequest)
        {
            var result = await _context.Coupons.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ID == couponRequest.ID);

            if (result == null)
            {
                var cupon = _mapper.Map<Coupon>(couponRequest);
                cupon.ID = GenerateID();
                await _context.Coupons.AddAsync(cupon);
            }
            else
            {
                var cupon = _mapper.Map<Coupon>(couponRequest);
                _context.Coupons.Update(cupon);
            }

            var check = await _context.SaveChangesAsync() > 0;
            if (check) return StatusCode(StatusCodes.Status201Created);

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        public async Task<object> RemoveAsync(string id)
        {
            var result = await _context.Coupons.FirstOrDefaultAsync(x => x.ID == id);

            if (result == null) return NotFound();

            _context.Coupons.Remove(result);
            var check = await _context.SaveChangesAsync() > 0;

            if (check) return StatusCode(StatusCodes.Status201Created);

            return StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}
