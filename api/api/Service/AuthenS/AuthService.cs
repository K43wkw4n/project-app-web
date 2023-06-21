using api.Data;
using api.DTOs; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;  

namespace api.Service.AuthenS
{
    public class AuthService : ControllerBase, IAuthService
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(Context context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<User>> GetUserAsync()
        {
            return await _context.Users
                .Include(x => x.Role)
                .Include(x => x.Address).ToListAsync();
        }
        
        public async Task<object> GetUserByIdAsync(int id)
        {
            var result = await _context.Users.Include(x => x.Role).Include(a => a.Address).FirstOrDefaultAsync(x => x.ID == id);
            if (result == null) return NotFound();

            return result;
        }

        public async Task<object> RegisterAsync(RegisterDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.Username);

            if (user != null)
                return BadRequest($"{request.Username} has already exist");
             
            string passwordHash
            = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var User = new User
            {
                UserName = request.Username,
                Password = passwordHash,
                Email = request.Email, 
                Coin = 0,
                RoleId = 2, 

            };

            await _context.Users.AddAsync(User);
            var check = await _context.SaveChangesAsync() > 0;

            if (check) return StatusCode(StatusCodes.Status201Created);

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        public async Task<object> LoginAsunc(LoginDto request)
        {
            var user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.UserName.Equals(request.Username)
                        || x.Email.Equals(request.Username));

            if (user == null) { return NotFound(); }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password)) { return BadRequest("Password Wrong"); }

            var userDto = new UserDto
            {
                Email = user.Email,
                Token = CreateToken(user),
            };

            return new { userDto, StatusCode = StatusCode(StatusCodes.Status200OK) };
        }

        private string CreateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"])); // ที่อยู่ securityKey
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature); // (1,2) 1. รหัสลับ 2. เข้ารหัส
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Role,user.Role.Name),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<object> RemoveUserAsync(int id)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.ID == id);

            if (result == null) return NotFound();

            _context.Users.Remove(result);
            var check = await _context.SaveChangesAsync() > 0;

            if (check) return StatusCode(StatusCodes.Status201Created);

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        public async Task<object> RePasswordAsync(RePasswordDto rePasswordDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.ID == rePasswordDto.UserId);

            if (!BCrypt.Net.BCrypt.Verify(rePasswordDto.OldPassword, user.Password)) { return BadRequest("Password Wrong"); }
             
            string password = BCrypt.Net.BCrypt.HashPassword(rePasswordDto.NewPassword);

            user.Password = password; 

            var check = await _context.SaveChangesAsync() > 0;

            if (check) return StatusCode(StatusCodes.Status201Created);

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        public object GetDeCodeTokenClaim()
        {
            var username = string.Empty;
            var role = string.Empty;

            if (_httpContextAccessor.HttpContext != null)
            {
                username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
                role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
                var userToken = new { username, role };

                return new { userToken, StatusCode = StatusCode(StatusCodes.Status200OK) };
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }
 
        public object getDecodeTokenAsync(string token)
        {             
            var handler = new JwtSecurityTokenHandler();
            var decodetoken = handler.ReadJwtToken(token);

            if(decodetoken != null)
            {  
                var tokenExp = decodetoken.Claims.First(claim => claim.Type.Equals("exp")).Value;
                var time = long.Parse(tokenExp);
                var tokenDate = DateTimeOffset.FromUnixTimeSeconds(time).UtcDateTime;

                var now = DateTime.Now.ToUniversalTime();

                var exp = tokenDate >= now;
                 
                return new { decodetoken.Payload, time, exp, StatusCode = StatusCode(StatusCodes.Status200OK) }; 
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        //public static long GetTokenExpirationTime(string token)
        //{
        //    var handler = new JwtSecurityTokenHandler();
        //    var jwtSecurityToken = handler.ReadJwtToken(token);
        //    var tokenExp = jwtSecurityToken.Claims.First(claim => claim.Type.Equals("exp")).Value;
        //    var ticks = long.Parse(tokenExp);
        //    return ticks;
        //}

    }
}
