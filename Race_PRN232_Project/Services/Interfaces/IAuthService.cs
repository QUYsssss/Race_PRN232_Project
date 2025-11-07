using Race_PRN232_Project.DTOs;

namespace Race_PRN232_Project.Services.Interfaces
{
    public interface IAuthService
    {
        TokenResponseDTO? Authenticate(string email, string password);
        bool Register(RegisterDTO dto);
    }
}
