using api.Data;
using api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Service.FavoriteS
{
    public class FavoriteService : ControllerBase, IFavoriteService
    {
        private readonly Context _context;

        public FavoriteService(Context context)
        {
            _context = context;
        }
        
        public async Task<Favorite> GetFavoriteByIdAsync(int userId)
        { 
            return await _context.Favorites.Include(x => x.Product).FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<object> AddToFavoriteAsync(FavoriteDto addFavDto)
        {
            var result = await _context.Favorites.FirstOrDefaultAsync(x => x.ProductId == addFavDto.ProductId);
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.ID == addFavDto.UserId);

            if(user == null) { return NotFound(); }

            if (result == null)
            {
                var fav = new Favorite
                {
                    ProductId = addFavDto.ProductId = addFavDto.ProductId,
                    UserId = addFavDto.UserId,
                };
                await _context.Favorites.AddAsync(fav);
            } 

            var check = await _context.SaveChangesAsync() > 0;
            if (check) return StatusCode(StatusCodes.Status201Created);

            return "you have already added";
        }

        public async Task<object> RemoveAsync(FavoriteDto favoriteDto)
        {
            var result = await _context.Favorites
                .FirstOrDefaultAsync(x => x.ProductId == favoriteDto.ProductId
                && x.UserId == favoriteDto.UserId);

            if (result == null) return NotFound();

            _context.Favorites.Remove(result);
            var check = await _context.SaveChangesAsync() > 0;

            if (check) return StatusCode(StatusCodes.Status201Created);

            return StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}
