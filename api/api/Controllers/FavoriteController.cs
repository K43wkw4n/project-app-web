using api.DTOs;
using api.Service.FavoriteS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetFavorite(int userId)
           => Ok(await _favoriteService.GetFavoriteByIdAsync(userId));

        [HttpPost("[action]")]
        public async Task<IActionResult> AddToFavorite(FavoriteDto favoriteDto)
            => Ok(await _favoriteService.AddToFavoriteAsync(favoriteDto));

        [HttpDelete("[action]")]
        public async Task<IActionResult> Remove(FavoriteDto favoriteDto)
            => Ok(await _favoriteService.RemoveAsync(favoriteDto));


    }
}
