using api.DTOs;
using api.Service.AddressS;
using api.Service.AuthenS;
using Hanssens.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IAddressService _addressService;

        public AuthController(IAuthService authService, IAddressService addressService)
        {
            _authService = authService;
            _addressService = addressService;
        }

        //-----------------------------------User-----------------------------------//

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUser()
            => Ok(await _authService.GetUserAsync());

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserById(int id)
            => Ok(await _authService.GetUserByIdAsync(id));

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterDto request)
            => Ok(await _authService.RegisterAsync(request));

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDto request)
            => Ok(await _authService.LoginAsunc(request));

        [HttpPost("[action]")]
        public async Task<IActionResult> Repassword(RePasswordDto rePasswordDto)
            => Ok(await _authService.RePasswordAsync(rePasswordDto));

        [HttpDelete("[action]")]
        public async Task<IActionResult> RemoveUser(int id)
            => Ok(await _authService.RemoveUserAsync(id));


        //-----------------------------------Address-----------------------------------//

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAddress()
            => Ok(await _addressService.GetAddressAsync());

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUpdateAddress(AddressDto addressDto)
            => Ok(await _addressService.CreateAndUpdateAddressAsync(addressDto));

        [HttpDelete("[action]")]
        public async Task<IActionResult> Remove(int id)
            => Ok(await _addressService.RemoveAsync(id));

        //-----------------------------------Address-----------------------------------//

        [HttpGet("[action]")]
        public async Task<IActionResult> GetStorageTest()
        {
            // initialize, with default settings
            var storage = new LocalStorage();
            storage.Store("123", "asd");

            return Ok(storage.Get("123"));
        }

        [HttpGet("[action]"), Authorize]
        public async Task<IActionResult> GetUserToken()
            => Ok(_authService.GetDeCodeTokenClaim());

        [HttpGet("[action]")]
        public ActionResult GetDecodeToken(string token)
            => Ok(_authService.getDecodeTokenAsync(token));

        [HttpGet("[action]"), Authorize]
        public async Task<IActionResult> GetTokenAsync()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (accessToken == null)
            {
                accessToken = "Not Login";
            }
            return Ok(accessToken);
        }

    }
}
