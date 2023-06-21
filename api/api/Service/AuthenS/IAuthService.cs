using api.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace api.Service.AuthenS
{
    public interface IAuthService
    {
        Task<List<User>> GetUserAsync();
        Task<Object> GetUserByIdAsync(int id);
        Task<Object> RePasswordAsync(RePasswordDto rePasswordDto);
        Task<Object> RegisterAsync(RegisterDto request);
        Task<Object> LoginAsunc(LoginDto request);
        Task<Object> RemoveUserAsync(int id);
        Object GetDeCodeTokenClaim();
        object getDecodeTokenAsync(string token);
    }
}
    