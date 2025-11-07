using Race_PRN232_Project.DTOs;

namespace Race_PRN232_Project.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetAll();
        UserDTO? GetById(int id);
        bool Create(UserDTO dto);
        bool Update(UserDTO dto);
        bool Delete(int id);
    }
}
