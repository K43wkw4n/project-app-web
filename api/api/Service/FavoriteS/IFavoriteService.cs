using api.DTOs;

namespace api.Service.FavoriteS
{
    public interface IFavoriteService
    {
        Task<Favorite> GetFavoriteByIdAsync(int userId);
        Task<Object> AddToFavoriteAsync(FavoriteDto favoriteDto);
        Task<Object> RemoveAsync(FavoriteDto favoriteDto);

    }
}
