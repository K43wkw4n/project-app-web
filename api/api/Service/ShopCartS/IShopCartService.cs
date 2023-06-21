using api.DTOs;

namespace api.Service.ShopCartS
{
    public interface IShopCartService
    {
        Task<List<ShopCart>> GetInCartAsync();
        Task<Object> AddToCartAsync(AddToCartDto addToCartDto);
        Task<object> RemoveItemToCartAsync(AddToCartDto addToCartDto);
    }
}
