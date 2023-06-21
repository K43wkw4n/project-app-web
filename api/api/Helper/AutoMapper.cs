using api.DTOs.CouponR;
using api.DTOs.ProductR;
using AutoMapper;

namespace api.Helper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            //Coupon
            CreateMap<Coupon, CouponRequest>();
            CreateMap<CouponRequest, Coupon>();
            
            CreateMap<Coupon, CouponResponse>();
            CreateMap<CouponResponse, Coupon>();

            //Product
            CreateMap<Product, ProductRequest>();
            CreateMap<ProductRequest, Product>();

            CreateMap<Product, ProductResponse>();
            CreateMap<ProductResponse, Product>();

        }
    }
}
