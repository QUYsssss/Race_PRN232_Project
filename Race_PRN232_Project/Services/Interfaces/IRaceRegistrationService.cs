using Race_PRN232_Project.DTOs;

namespace Race_PRN232_Project.Services.Interfaces
{
    public interface IRaceRegistrationService
    {
        void Register(int userId, RaceRegistrationDto dto);
    }
}
