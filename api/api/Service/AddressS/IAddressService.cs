using api.DTOs;
using api.DTOs.CouponR;

namespace api.Service.AddressS
{
    public interface IAddressService
    {
        Task<List<Address>> GetAddressAsync();
        Task<Object> CreateAndUpdateAddressAsync(AddressDto addressDto);
        Task<Object> RemoveAsync(int id); 

    }
}
