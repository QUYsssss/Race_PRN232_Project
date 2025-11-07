using Microsoft.EntityFrameworkCore;
using Race_PRN232_Project.DTOs;
using Race_PRN232_Project.Helpers;
using Race_PRN232_Project.Models;
using Race_PRN232_Project.Services.Interfaces;
using System.Security.Claims;

namespace Race_PRN232_Project.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly RacePRN232Context _context;
        private readonly IConfiguration _config;

        public AuthService(RacePRN232Context context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // ------------------- LOGIN -------------------
        public TokenResponseDTO? Authenticate(string email, string password)
        {
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == email);
            if (user == null || user.PasswordHash != password)
                return null;

            // ✅ Tạo danh sách Claims cho token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role?.RoleName ?? "User")
            };

            // ✅ Sinh JWT token thật từ JwtHelper
            var token = JwtHelper.GenerateToken(claims, _config);

            return new TokenResponseDTO
            {
                Token = token,
                ExpireAt = DateTime.UtcNow.AddMinutes(double.Parse(_config["JWT:ExpireMinutes"] ?? "120")),
                User = new UserDTO
                {
                    UserId = user.UserId,
                    FullName = $"{user.FirstName} {user.LastName}",
                    Email = user.Email,
                    RoleName = user.Role?.RoleName ?? "User"
                }
            };
        }

        // ------------------- REGISTER -------------------
        public bool Register(RegisterDTO dto)
        {
            // kiểm tra email đã tồn tại chưa
            if (_context.Users.Any(u => u.Email == dto.Email))
                return false;

            if (dto.Password != dto.ConfirmPassword)
                return false;

            // xác định RoleId theo tên
            var role = _context.Roles.FirstOrDefault(r => r.RoleName == dto.Role);
            if (role == null)
                return false;

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash = dto.Password,
                RoleId = role.RoleId,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }
    }
}
