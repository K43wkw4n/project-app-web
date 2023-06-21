using api.Data;
using api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Service.ShopCartS
{
    public class ShopCartService : ControllerBase, IShopCartService
    {
        private readonly Context _context;

        private string GenerateID() => Guid.NewGuid().ToString("N");
        public ShopCartService(Context context)
        {
            _context = context; 
        }

        public async Task<List<ShopCart>> GetInCartAsync()
        {
            return await _context.ShopCarts.Include(x => x.Items).ThenInclude(x => x.Product).ToListAsync();
        }

        public async Task<object> AddToCartAsync(AddToCartDto addToCartDto)
        {
            var cart = await RetrieveShopCart(addToCartDto.userId);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.ID == addToCartDto.userId);
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ID.Equals(addToCartDto.productId));

            if (product is null || user is null) return NotFound();

            if (cart is null) cart = await CreateCart(addToCartDto.userId);

            cart.AddItem(product, addToCartDto.quantity);

            return await returnStatusAsync();
        }

        public async Task<object> RemoveItemToCartAsync(AddToCartDto addToCartDto)
        {
            var cart = await RetrieveShopCart(addToCartDto.userId);

            if (cart == null) return NotFound();

            cart.RemoveItem(addToCartDto.productId, addToCartDto.quantity);

            return await returnStatusAsync();
        }

        //-----------------------------------------Helper--------------------------------------------//

        public async Task<ShopCart> CreateCart(int userId)
        {
            ShopCart shopCart = new();
            if (await _context.ShopCarts.SingleOrDefaultAsync(x => x.UserId == userId) == null) shopCart = new ShopCart { UserId = userId, createDate = DateTime.Now };
            await _context.ShopCarts.AddAsync(shopCart);
            return shopCart;
        }

        private async Task<ShopCart> RetrieveShopCart(int userId)
        {
            var cart = await _context.ShopCarts
                   .Include(i => i.Items)
                   .ThenInclude(p => p.Product) 
                   .SingleOrDefaultAsync(x => x.UserId == userId);
            return cart;
        }

        public async Task<StatusCodeResult> returnStatusAsync()
        {
            var isSuccess = await _context.SaveChangesAsync() > 0;

            if (isSuccess) return StatusCode(StatusCodes.Status201Created);

            return StatusCode(StatusCodes.Status400BadRequest);
        }


    }
}
