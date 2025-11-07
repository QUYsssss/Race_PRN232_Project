using Race_PRN232_Project.DTOs;
using Race_PRN232_Project.Models;
using Race_PRN232_Project.Services.Interfaces;

namespace Race_PRN232_Project.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly RacePRN232Context _context;

        public AuthService(RacePRN232Context context)
        {
            _context = context;
        }

        public TokenResponseDTO? Authenticate(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);
            if (user == null) return null;

            return new TokenResponseDTO
            {
                AccessToken = "mock-token",
                ExpiredAt = DateTime.Now.AddHours(2),
                RefreshToken = Guid.NewGuid().ToString()
            };
        }

        public bool Register(RegisterDTO dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email)) return false;
            if (dto.Password != dto.ConfirmPassword) return false;

            int roleId = dto.Role.ToLower() switch
            {
                "runner" => 2,
                "collaborator" => 4,
                _ => 2
            };

            var newUser = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash = dto.Password,
                RoleId = roleId,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return true;
        }
    }
}
